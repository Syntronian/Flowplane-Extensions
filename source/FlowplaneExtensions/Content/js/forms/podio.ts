﻿/// <reference path="../refs.d.ts" />

module fpxt.forms {

    export class Podio implements IForm {

        public extId: string = 'PODIO';

        public setup(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            $("#assignees-loading").show();
            $("#workspaces-loading").show();
            $("#cboActivityParamAssignee").hide();
            $("#cboActivityParamWorkspace").hide();
            $("#cboActivityParamApp").empty();
            $("#cboActivityParamItem").empty();
            shearnie.tools.html.fillCombo($("#cboActivityParamApp"), null, "Select workspace above");
            shearnie.tools.html.fillCombo($("#cboActivityParamItem"), null, "Select app above");

            // get data first
            var pd = new Array();
            pd.push(new shearnie.tools.PostData(fpxt.BaseApiUrl.corepath + 'api/oauth/getpodioassignees', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));
            pd.push(new shearnie.tools.PostData(fpxt.BaseApiUrl.corepath + 'api/oauth/getpodioworkspaces', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

            var cd: shearnie.tools.html.comboData[] = [];
            new shearnie.tools.Poster().SendAsync(pd, numErrs => {
                // fill assignees
                cd.push({
                    getItems: () => {
                        var ret: shearnie.tools.html.comboItem[] = [];
                        pd[0].result.items.forEach(item => {
                            ret.push({ value: item.id, display: item.name });
                        });
                        return ret;
                    }
                });

                shearnie.tools.html.fillCombo($("#cboActivityParamAssignee"), cd, "Select assignee");
                $("#assignees-loading").hide();
                $("#cboActivityParamAssignee").show();


                // fill projects
                cd = [];
                cd.push({
                    getItems: () => {
                        var ret: shearnie.tools.html.comboItem[] = [];
                        pd[1].result.items.forEach(item => {
                            ret.push({ value: item.id, display: item.name });
                        });
                        return ret;
                    }
                });

                shearnie.tools.html.fillCombo($("#cboActivityParamWorkspace"), cd, "Select workspace");
                $("#workspaces-loading").hide();
                $("#cboActivityParamWorkspace").show();

                onCompleted();
            });

            // load projects and workspace selected
            $("#cboActivityParamWorkspace").change(event => {
                this.load_Combo(authKeys, 'cboActivityParamWorkspace', 'spaceId', 'cboActivityParamApp', 'app', 'api/oauth/getpodioapps');
            });

            $("#cboActivityParamApp").change(event => {
                this.load_Combo(authKeys, 'cboActivityParamApp', 'appId', 'cboActivityParamItem', 'items', 'api/oauth/getpodioitems');
            });
        }

        private load_Combo(authKeys: fpxtParam[], source: string, sourceKey: string, target: string, loading: string, method: string) {
            $("#" + target).empty();
            $("#" + target).append($('<option>Loading ' + loading + '...</option>').attr("value", '').attr("disabled", 'disabled').attr("selected", 'selected'));
            var result = new shearnie.tools.Poster().SendSync(
                fpxt.BaseApiUrl.corepath + method,
                {
                    extId: this.extId,
                    authKeys: JSON.stringify(authKeys),
                    objParams: JSON.stringify([
                        {
                            key: sourceKey,
                            value: $('#' + source).val()
                        }])
                });

            if (result.items.length == 0) {
                shearnie.tools.html.fillCombo($("#" + target), null, "No " + loading);
                return;
            }

            var cd = [];
            cd.push({
                getItems: () => {
                    var ret: shearnie.tools.html.comboItem[] = [];
                    result.items.forEach(item => {
                        ret.push({ value: item.id, display: item.name });
                    });
                    return ret;
                }
            });
            shearnie.tools.html.fillCombo($("#" + target), cd, "Select " + loading);
        }

        public setupAuthPre(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }

        public setupAuthPost(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
            if ($('#txtPodioAppId').val() != '' && $('#txtPodioAppSecret').val() != '') {
                var bswitch: any = $('#chkPodioOauthOption');
                bswitch.bootstrapSwitch('toggleState');
            }
        }

        public load_orgs(setval?: string) {
            $("#podio-orgs-loading").show();
            $("#cboPodioOrg").hide();
            $("#cboPodioOrg").empty();
            var result = new shearnie.tools.Poster().SendSync(
                fpxt.BaseApiUrl.corepath + 'api/oauth/getpodioorgs',
                {
                    extId: this.extId,
                    authKeys: JSON.stringify([
                        {
                            key: 'clientId',
                            value: $('#txtPodioAppId').val()
                        },
                        {
                            key: 'clientSecret',
                            value: $('#txtPodioAppSecret').val()
                        },
                        {
                            key: 'accessToken',
                            value: $('#txtPodioAccessToken').val()
                        }]),
                    objParams: JSON.stringify('')
                });

            if (result.items.length == 0) {
                shearnie.tools.html.fillCombo($("#cboPodioOrg"), null, "No orgs");
                return;
            }

            var cd = [];
            cd.push({
                getItems: () => {
                    var ret: shearnie.tools.html.comboItem[] = [];
                    result.items.forEach(item => {
                        ret.push({ value: item.id, display: item.name });
                    });
                    return ret;
                }
            });
            shearnie.tools.html.fillCombo($("#cboPodioOrg"), cd, "Select organisation");

            if (setval != null)
                $("#cboPodioOrg").val(setval);
            $("#podio-orgs-loading").hide();
            $("#cboPodioOrg").show();
        }

        public fill(baseApiUrl: string, authKeys: fpxtParam[], values: fpxtParam[]) {
            $("#txtActivityParamTaskDesc").val('');
            $("#txtActivityParamTaskDueDays").val('');
            $("#cboActivityParamAssignee").val(null);
            $("#cboActivityParamWorkspace").val(null);
            $("#cboActivityParamApp").val(null);
            $("#cboActivityParamItem").val(null);

            if (values == null) return;
            values.forEach((p) => {
                switch (p.key) {
                    case 'taskdesc':
                        $("#txtActivityParamTaskDesc").val(p.value);
                        break;
                    case 'taskduedays':
                        $("#txtActivityParamTaskDueDays").val(p.value);
                        break;
                    case 'taskassignee':
                        $("#cboActivityParamAssignee").val(p.value);
                        break;
                    case 'taskworkspace':
                        $("#cboActivityParamWorkspace").val(p.value);
                        break;
                }
            });

            this.load_Combo(authKeys, 'cboActivityParamWorkspace', 'spaceId', 'cboActivityParamApp', 'app', 'api/oauth/getpodioapps');
            values.forEach((p) => {
                switch (p.key) {
                    case 'taskapp':
                        $("#cboActivityParamApp").val(p.value);
                }
            });

            this.load_Combo(authKeys, 'cboActivityParamApp', 'appId', 'cboActivityParamItem', 'items', 'api/oauth/getpodioitems');
            values.forEach((p) => {
                switch (p.key) {
                    case 'taskitem':
                        $("#cboActivityParamItem").val(p.value);
                }
            });
        }

        public getProperties(): fpxtParam[] {
            if ($("#txtActivityParamTaskDesc").val() == "")
                throw "Description is required.";

            var ret: fpxtParam[] = [];

            ret.push({ key: "taskdesc", value: $("#txtActivityParamTaskDesc").val() });
            ret.push({ key: "taskduedays", value: $("#txtActivityParamTaskDueDays").val() });

            if ($("#cboActivityParamAssignee").val()) {
                ret.push({ key: "taskassignee", value: $("#cboActivityParamAssignee").val() });
                ret.push({ key: "taskassigneename", value: $("#cboActivityParamAssignee option:selected").text() });
            }

            if ($("#cboActivityParamWorkspace").val()) {
                ret.push({ key: "taskworkspace", value: $("#cboActivityParamWorkspace").val() });
                ret.push({ key: "taskworkspacename", value: $("#cboActivityParamWorkspace option:selected").text() });
            }

            if ($("#cboActivityParamApp").val()) {
                ret.push({ key: "taskapp", value: $("#cboActivityParamApp").val() });
                ret.push({ key: "taskappname", value: $("#cboActivityParamApp option:selected").text() });
            }

            if ($("#cboActivityParamItem").val()) {
                ret.push({ key: "taskitem", value: $("#cboActivityParamItem").val() });
                ret.push({ key: "taskitemname", value: $("#cboActivityParamItem option:selected").text() });
            }

            return ret;
        }
    }
} 