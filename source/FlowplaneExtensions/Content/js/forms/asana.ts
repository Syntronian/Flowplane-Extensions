/// <reference path="../refs.d.ts" />

module fpxt.forms {

    export class Asana {

        public static extId: string = 'ASANA';

        constructor(public authKeys: fpxtParam[], public objParams: fpxtParam[]) {
            $("#assignees-loading").show();
            $("#workspaces-loading").show();
            $("#cboActivityParamAssignee").hide();
            $("#cboActivityParamWorkspace").hide();

            // get data first
            var baseUrl = 'http://localhost/flowplaneextensions/';
            var pd = new Array();
            pd.push(new shearnie.tools.PostData(baseUrl + 'api/process/getassignees', { extId: Asana.extId, authKeys: JSON.stringify(authKeys) }));
            pd.push(new shearnie.tools.PostData(baseUrl + 'api/process/getworkspaces', { extId: Asana.extId, authKeys: JSON.stringify(authKeys) }));

            new shearnie.tools.Poster().SendAsync(pd, numErrs => {
                // fill assignees
                var cd: shearnie.tools.html.comboData[] = [];
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
                shearnie.tools.html.fillCombo($("#cboActivityParamProject"), null, "Select workspace above");
                $("#workspaces-loading").hide();
                $("#cboActivityParamWorkspace").show();
            });
        }

    }
}
 