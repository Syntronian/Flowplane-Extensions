/// <reference path="../refs.d.ts" />nj cjcb
var fpxt;
(function (fpxt) {
    (function (forms) {
        var Paymo = (function () {
            function Paymo() {
            }
            Paymo.setup = function (baseApiUrl, authKeys, objParams, onCompleted) {
                $("#assignees-loading").show();
                $("#cboActivityParamAssignee").hide();
                $("#cboActivityParamProject").empty();
                shearnie.tools.html.fillCombo($("#cboActivityParamProject"), null);

                // get data first
                var pd = new Array();
                pd.push(new shearnie.tools.PostData(baseApiUrl + 'api/process/getassignees', { extId: Paymo.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

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

                    onCompleted();
                });

                // load projects and task lists selected
                Paymo.loadProjects(baseApiUrl, authKeys);
                Paymo.load_taskLists(baseApiUrl, authKeys);
            };

            Paymo.loadProjects = function (baseApiUrl, authKeys) {
                $("#cboActivityParamProject").empty();
                $("#cboActivityParamProject").append($('<option>Loading projects...</option>').attr("value", '').attr("disabled", 'disabled').attr("selected", 'selected'));

                var result = new shearnie.tools.Poster().SendSync(baseApiUrl + 'api/process/getprojects', {
                    extId: Paymo.extId,
                    authKeys: JSON.stringify(authKeys)
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

            Paymo.load_taskLists = function (baseApiUrl, authKeys) {
                $("#cboActivityParamTaskList").empty();
                $("#cboActivityParamTaskList").append($('<option>Loading task lists...</option>').attr("value", '').attr("disabled", 'disabled').attr("selected", 'selected'));
                var result = new shearnie.tools.Poster().SendSync(baseApiUrl + 'api/process/gettasks', {
                    extId: Paymo.extId,
                    authKeys: JSON.stringify(authKeys)
                });

                if (result.items.length == 0) {
                    shearnie.tools.html.fillCombo($("#cboActivityParamProject"), null, "No task lists");
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
                shearnie.tools.html.fillCombo($("#cboActivityParamTaskList"), cd, "Select task");
            };

            Paymo.fill = function (baseApiUrl, authKeys, values) {
                $("#txtActivityParamTaskDesc").val('');
                $("#txtActivityParamTaskDueDays").val('');
                $("#cboActivityParamAssignee").val(null);
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
                    }
                });

                Paymo.loadProjects(baseApiUrl, authKeys);
                Paymo.load_taskLists(baseApiUrl, authKeys);

                values.forEach(function (p) {
                    switch (p.key) {
                        case 'taskproject':
                            $("#cboActivityParamProject").val(p.value);
                            break;
                        case 'tasktasklist':
                            $("#cboActivityParamProject").val(p.value);
                    }
                });
            };

            Paymo.getProperties = function () {
                if ($("#txtActivityParamTaskDesc").val() == "")
                    throw "Description is required.";

                var ret = [];

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
            };
            Paymo.extId = 'PAYMO';
            return Paymo;
        })();
        forms.Paymo = Paymo;
    })(fpxt.forms || (fpxt.forms = {}));
    var forms = fpxt.forms;
})(fpxt || (fpxt = {}));
