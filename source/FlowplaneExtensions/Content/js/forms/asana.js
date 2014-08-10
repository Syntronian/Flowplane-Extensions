/// <reference path="../refs.d.ts" />
var fpxt;
(function (fpxt) {
    (function (forms) {
        var Asana = (function () {
            function Asana() {
            }
            Asana.setup = function (baseApiUrl, authKeys, objParams, onCompleted) {
                $("#assignees-loading").show();
                $("#workspaces-loading").show();
                $("#cboActivityParamAssignee").hide();
                $("#cboActivityParamWorkspace").hide();
                $("#cboActivityParamProject").empty();
                shearnie.tools.html.fillCombo($("#cboActivityParamProject"), null, "Select workspace above");

                // get data first
                var pd = new Array();
                pd.push(new shearnie.tools.PostData(baseApiUrl + 'api/process/getassignees', { extId: Asana.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));
                pd.push(new shearnie.tools.PostData(baseApiUrl + 'api/process/getworkspaces', { extId: Asana.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

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
                    $("#workspaces-loading").hide();
                    $("#cboActivityParamWorkspace").show();

                    onCompleted();
                });

                // load projects and workspace selected
                $("#cboActivityParamWorkspace").change(function (event) {
                    Asana.loadProjects(baseApiUrl, authKeys);
                });
            };

            Asana.loadProjects = function (baseApiUrl, authKeys) {
                $("#cboActivityParamProject").empty();
                $("#cboActivityParamProject").append($('<option>Loading projects...</option>').attr("value", '').attr("disabled", 'disabled').attr("selected", 'selected'));

                var result = new shearnie.tools.Poster().SendSync(baseApiUrl + 'api/process/getprojects', {
                    extId: Asana.extId,
                    authKeys: JSON.stringify(authKeys),
                    objParams: JSON.stringify([{
                            key: 'workspaceId',
                            value: $("#cboActivityParamWorkspace").val()
                        }])
                });

                if (result.items.length == 0) {
                    shearnie.tools.html.fillCombo($("#cboActivityParamProject"), null, "No projects");
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
                shearnie.tools.html.fillCombo($("#cboActivityParamProject"), cd, "Select project");
            };

            Asana.fill = function (baseApiUrl, authKeys, values) {
                $("#txtActivityParamTaskDesc").val('');
                $("#txtActivityParamTaskDueDays").val('');
                $("#cboActivityParamAssignee").val(null);
                $("#cboActivityParamWorkspace").val(null);
                $("#cboActivityParamProject").val(null);

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

                Asana.loadProjects(baseApiUrl, authKeys);

                values.forEach(function (p) {
                    switch (p.key) {
                        case 'taskproject':
                            $("#cboActivityParamProject").val(p.value);
                            break;
                    }
                });
            };

            Asana.getProperties = function () {
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

                if ($("#cboActivityParamProject").val()) {
                    ret.push({ key: "taskproject", value: $("#cboActivityParamProject").val() });
                    ret.push({ key: "taskprojectname", value: $("#cboActivityParamProject option:selected").text() });
                }

                return ret;
            };
            Asana.extId = 'ASANA';
            return Asana;
        })();
        forms.Asana = Asana;
    })(fpxt.forms || (fpxt.forms = {}));
    var forms = fpxt.forms;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=asana.js.map
