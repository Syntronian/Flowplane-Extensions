/// <reference path="../refs.d.ts" />
var fpxt;
(function (fpxt) {
    (function (forms) {
        var Asana = (function () {
            function Asana(authKeys, objParams) {
                this.authKeys = authKeys;
                this.objParams = objParams;
                $("#assignees-loading").show();
                $("#workspaces-loading").show();
                $("#cboActivityParamAssignee").hide();
                $("#cboActivityParamWorkspace").hide();

                // get data first
                var baseUrl = 'http://localhost/flowplaneextensions/';
                var pd = new Array();
                pd.push(new shearnie.tools.PostData(baseUrl + 'api/process/getassignees', { extId: Asana.extId, authKeys: JSON.stringify(authKeys) }));
                pd.push(new shearnie.tools.PostData(baseUrl + 'api/process/getworkspaces', { extId: Asana.extId, authKeys: JSON.stringify(authKeys) }));

                new shearnie.tools.Poster().SendAsync(pd, function (numErrs) {
                    // fill assignees
                    var cd = [];
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

                    // fill workspaces
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
                    shearnie.tools.html.fillCombo($("#cboActivityParamProject"), null, "Select workspace above");
                    $("#workspaces-loading").hide();
                    $("#cboActivityParamWorkspace").show();
                });
            }
            Asana.extId = 'ASANA';
            return Asana;
        })();
        forms.Asana = Asana;
    })(fpxt.forms || (fpxt.forms = {}));
    var forms = fpxt.forms;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=asana.js.map
