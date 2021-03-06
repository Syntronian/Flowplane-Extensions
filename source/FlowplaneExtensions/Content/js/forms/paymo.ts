﻿/// <reference path="../refs.d.ts" />

module fpxt.forms {

    export class Paymo implements IForm {

        public extId: string = 'PAYMO';

        public setup(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            $("#assignees-loading").show();
            $("#projects-loading").show();
            $("#cboActivityParamAssignee").hide();
            $("#cboActivityParamProject").hide();
            $("#cboActivityParamTaskList").empty();
            shearnie.tools.html.fillCombo($("#cboActivityParamTaskList"), null, "Select project above");

            // get data first
            var pd = new Array();
            pd.push(new shearnie.tools.PostData(baseApiUrl + 'api/process/getassignees', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));
            pd.push(new shearnie.tools.PostData(baseApiUrl + 'api/process/getprojects', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

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

                shearnie.tools.html.fillCombo($("#cboActivityParamProject"), cd, "Select project");
                $("#projects-loading").hide();
                $("#cboActivityParamProject").show();

                onCompleted();
            });

            // load projects and workspace selected
            $("#cboActivityParamProject").change(event => {
                this.load_taskLists(baseApiUrl, authKeys);
            });
        }

        private load_taskLists(baseApiUrl: string, authKeys: fpxtParam[]) {
            $("#cboActivityParamTaskList").empty();
            $("#cboActivityParamTaskList").append($('<option>Loading task lists...</option>').attr("value", '').attr("disabled", 'disabled').attr("selected", 'selected'));
            var result = new shearnie.tools.Poster().SendSync(
                baseApiUrl + 'api/process/gettasks',
                {
                    extId: this.extId,
                    authKeys: JSON.stringify(authKeys),
                    objParams: JSON.stringify([
                        {
                            key: 'project_id',
                            value: $("#cboActivityParamProject").val()
                        }])
                });

            if (result.items.length == 0) {
                shearnie.tools.html.fillCombo($("#cboActivityParamTaskList"), null, "No task lists");
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
            shearnie.tools.html.fillCombo($("#cboActivityParamTaskList"), cd, "Select task");
        }

        public setupAuthPre(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }

        public setupAuthPost(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }

        public fill(baseApiUrl: string, authKeys: fpxtParam[], values: fpxtParam[]) {
            $("#txtActivityParamTaskDesc").val('');
            $("#txtActivityParamTaskDueDays").val('');
            $("#cboActivityParamAssignee").val(null);
            $("#cboActivityParamProject").val(null);
            $("#cboActivityParamTaskList").val(null);

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
                    case 'taskproject':
                        $("#cboActivityParamProject").val(p.value);
                        break;
                }
            });

            this.load_taskLists(baseApiUrl, authKeys);

            values.forEach((p) => {
                switch (p.key) {
                    case 'tasktasklist':
                        $("#cboActivityParamTaskList").val(p.value);
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

            if ($("#cboActivityParamProject").val()) {
                ret.push({ key: "taskproject", value: $("#cboActivityParamProject").val() });
                ret.push({ key: "taskprojectname", value: $("#cboActivityParamProject option:selected").text() });
            }

            if ($("#cboActivityParamTaskList").val()) {
                ret.push({ key: "tasktasklist", value: $("#cboActivityParamTaskList").val() });
                ret.push({ key: "tasktasklistname", value: $("#cboActivityParamTaskList option:selected").text() });
            }

            return ret;
        }
    }
} 