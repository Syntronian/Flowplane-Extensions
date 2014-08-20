/// <reference path="../refs.d.ts" />
var fpxt;
(function (fpxt) {
    (function (forms) {
        var Podio = (function () {
            function Podio() {
                this.extId = 'PODIO';
            }
            Podio.prototype.setup = function (baseApiUrl, authKeys, objParams, onCompleted) {
                var _this = this;
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
                pd.push(new shearnie.tools.PostData(baseApiUrl + 'api/process/getassignees', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));
                pd.push(new shearnie.tools.PostData(baseApiUrl + 'api/process/getworkspaces', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

                var cd = [];
                new shearnie.tools.Poster().SendAsync(pd, function (numErrs) {
                    // fill assignees
                    cd.push({
                        getItems: function () {
                            var ret = [];
                            pd[0].result.items.forEach(function (item) {
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
                        getItems: function () {
                            var ret = [];
                            pd[1].result.items.forEach(function (item) {
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
                $("#cboActivityParamWorkspace").change(function (event) {
                    _this.load_Combo(baseApiUrl, authKeys, 'cboActivityParamWorkspace', 'spaceId', 'cboActivityParamApp', 'app', 'api/process/getapps');
                });

                $("#cboActivityParamApp").change(function (event) {
                    _this.load_Combo(baseApiUrl, authKeys, 'cboActivityParamApp', 'appId', 'cboActivityParamItem', 'items', 'api/process/getitems');
                });
            };

            Podio.prototype.load_Combo = function (baseApiUrl, authKeys, source, sourceKey, target, loading, method) {
                $("#" + target).empty();
                $("#" + target).append($('<option>Loading ' + loading + '...</option>').attr("value", '').attr("disabled", 'disabled').attr("selected", 'selected'));
                var result = new shearnie.tools.Poster().SendSync(baseApiUrl + method, {
                    extId: this.extId,
                    authKeys: JSON.stringify(authKeys),
                    objParams: JSON.stringify([{
                            key: sourceKey,
                            value: $("#" + source).val()
                        }])
                });

                if (result.items.length == 0) {
                    shearnie.tools.html.fillCombo($("#" + target), null, "No " + loading);
                    return;
                }

                var cd = [];
                cd.push({
                    getItems: function () {
                        var ret = [];
                        result.items.forEach(function (item) {
                            ret.push({ value: item.id, display: item.name });
                        });
                        return ret;
                    }
                });
                shearnie.tools.html.fillCombo($("#" + target), cd, "Select " + loading);
            };

            Podio.prototype.setupAuthPre = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            Podio.prototype.setupAuthPost = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
                if ($('#txtPodioAppId').val() != '' && $('#txtPodioAppSecret').val() != '') {
                    var bswitch = $('#chkPodioOauthOption');
                    bswitch.bootstrapSwitch('toggleState');
                }
            };

            Podio.prototype.load_orgs = function (setval) {
                $("#podio-orgs-loading").show();
                $("#cboPodioOrg").hide();
                $("#cboPodioOrg").empty();
                var result = new shearnie.tools.Poster().SendSync(fpxt.BaseApiUrl.corepath + 'api/oauth/getpodioorgs', {
                    ClientId: '',
                    ClientSecret: '',
                    AccessToken: $('#txtPodioAccessToken').val()
                });

                if (result.items.length == 0) {
                    shearnie.tools.html.fillCombo($("#cboPodioOrg"), null, "No orgs");
                    return;
                }

                var cd = [];
                cd.push({
                    getItems: function () {
                        var ret = [];
                        result.items.forEach(function (item) {
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
            };

            Podio.prototype.fill = function (baseApiUrl, authKeys, values) {
                $("#txtActivityParamTaskDesc").val('');
                $("#txtActivityParamTaskDueDays").val('');
                $("#cboActivityParamAssignee").val(null);
                $("#cboActivityParamWorkspace").val(null);
                $("#cboActivityParamApp").val(null);
                $("#cboActivityParamItem").val(null);

                if (values == null)
                    return;
                values.forEach(function (p) {
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

                this.load_Combo(baseApiUrl, authKeys, 'cboActivityParamWorkspace', 'spaceId', 'cboActivityParamApp', 'app', 'api/process/getapps');
                values.forEach(function (p) {
                    switch (p.key) {
                        case 'taskapp':
                            $("#cboActivityParamApp").val(p.value);
                    }
                });

                this.load_Combo(baseApiUrl, authKeys, 'cboActivityParamApp', 'appId', 'cboActivityParamItem', 'items', 'api/process/getitems');
                values.forEach(function (p) {
                    switch (p.key) {
                        case 'taskitems':
                            $("#cboActivityParamItem").val(p.value);
                    }
                });
            };

            Podio.prototype.getProperties = function () {
                if ($("#txtActivityParamTaskDesc").val() == "")
                    throw "Description is required.";

                var ret = [];

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
            };
            return Podio;
        })();
        forms.Podio = Podio;
    })(fpxt.forms || (fpxt.forms = {}));
    var forms = fpxt.forms;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=podio.js.map
