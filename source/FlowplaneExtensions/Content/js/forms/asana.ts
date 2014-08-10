﻿/// <reference path="../refs.d.ts" />

module fpxt.forms {

    export class Asana {

        public static extId: string = 'ASANA';

        constructor(public authKeys: fpxtParam[], public objParams: fpxtParam[]) {
            $("#assignees-loading").show();
            $("#workspaces-loading").show();
            $("#cboActivityParamAssignee").hide();
            $("#cboActivityParamWorkspace").hide();
            $("#cboActivityParamProject").empty();
            shearnie.tools.html.fillCombo($("#cboActivityParamProject"), null, "Select workspace above");

            // get data first
            var baseUrl = 'http://localhost/flowplaneextensions/';
            var pd = new Array();
            pd.push(new shearnie.tools.PostData(baseUrl + 'api/process/getassignees', { extId: Asana.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));
            pd.push(new shearnie.tools.PostData(baseUrl + 'api/process/getworkspaces', { extId: Asana.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

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


                // fill workspaces
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
            });

            // load projects and workspace selected
            $("#cboActivityParamWorkspace").change(event => {
                $("#cboActivityParamProject").empty();
                $("#cboActivityParamProject").append($('<option>Loading projects...</option>').attr("value", '').attr("disabled", 'disabled').attr("selected", 'selected'));

                var result = new shearnie.tools.Poster().SendSync(
                    baseUrl + 'api/process/getprojects',
                    {
                        extId: Asana.extId,
                        authKeys: JSON.stringify(authKeys),
                        objParams: JSON.stringify([
                            {
                                key: 'workspaceId',
                                value: $("#cboActivityParamWorkspace").val()
                            }])
                    });

                if (result.items.length == 0) {
                    shearnie.tools.html.fillCombo($("#cboActivityParamProject"), null, "No projects");
                    return;
                }

                cd = [];
                cd.push({
                    getItems: () => {
                        var ret: shearnie.tools.html.comboItem[] = [];
                        result.items.forEach(item => {
                            ret.push({ value: item.id, display: item.name });
                        });
                        return ret;
                    }
                });
                shearnie.tools.html.fillCombo($("#cboActivityParamProject"), cd, "Select project");
            });
        }
    }
}
 