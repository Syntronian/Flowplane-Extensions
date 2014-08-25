/// <reference path="../refs.d.ts" />
var fpxt;
(function (fpxt) {
    (function (forms) {
        var Wrike = (function () {
            function Wrike() {
                this.extId = 'WRIKE';
            }
            Wrike.prototype.setup = function (baseApiUrl, authKeys, objParams, onCompleted) {
                $("#assignees-loading").show();
                $("#cboActivityParamAssignee").hide();

                // get data first
                var pd = new Array();
                pd.push(new shearnie.tools.PostData(baseApiUrl + 'api/process/getassignees', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

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
            };

            Wrike.prototype.load_folders = function (checkedNodes) {
                var _this = this;
                $("#folders-loading").show();
                $("#treeActivityParamFolders").hide();

                this.selectedFolders = [];
                var tree = $('#treeActivityParamFolders');

                tree.on('changed.jstree', function (e, data) {
                    if (data.action == 'select_node') {
                        if (!Enumerable.from(_this.selectedFolders).any(function (i) {
                            return i == data.node.id;
                        }))
                            _this.selectedFolders.push(data.node.id);
                    } else if (data.action == 'deselect_node') {
                        if (Enumerable.from(_this.selectedFolders).any(function (i) {
                            return i == data.node.id;
                        }))
                            _this.selectedFolders.splice(_this.selectedFolders.indexOf(data.node.id), 1);
                    }
                }).jstree({
                    'checkbox': {
                        "three_state": false
                    },
                    'plugins': ["wholerow", "checkbox"],
                    'core': {
                        "themes": { "responsive": false },
                        'data': this.getKids("0")
                    }
                });

                // pre-select
                if (checkedNodes) {
                    checkedNodes.forEach(function (i) {
                        $.jstree.reference(i).select_node(i, true);
                    });
                    this.selectedFolders = checkedNodes;
                }

                $("#folders-loading").hide();
                $("#treeActivityParamFolders").show();
            };

            Wrike.prototype.getKids = function (parentId) {
                var _this = this;
                var ret = [];

                Enumerable.from(this.folders).where(function (i) {
                    return i.parentId == parentId && i.id != parentId;
                }).forEach(function (i) {
                    ret.push({
                        "id": i.id,
                        "text": i.title,
                        "children": _this.getKids(i.id)
                    });
                });

                return ret;
            };

            Wrike.prototype.setupAuthPre = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            Wrike.prototype.setupAuthPost = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            Wrike.prototype.fill = function (baseApiUrl, authKeys, values) {
                var _this = this;
                $("#txtActivityParamTaskDesc").val('');
                $("#cboActivityParamAssignee").val(null);

                if (values == null)
                    return;
                values.forEach(function (p) {
                    switch (p.key) {
                        case 'taskdesc':
                            $("#txtActivityParamTaskDesc").val(p.value);
                            break;
                        case 'taskfolders':
                            _this.selectedFolders = JSON.parse(p.value);
                            break;
                        case 'taskassignee':
                            $("#cboActivityParamAssignee").val(p.value);
                            break;
                    }
                });

                this.load_folders(this.selectedFolders);
            };

            Wrike.prototype.getProperties = function () {
                if ($("#txtActivityParamTaskDesc").val() == "")
                    throw "Description is required.";

                var ret = [];
                if ($("#cboActivityParamAssignee").val()) {
                    ret.push({ key: "taskassignee", value: $("#cboActivityParamAssignee").val() });
                    ret.push({ key: "taskassigneename", value: $("#cboActivityParamAssignee option:selected").text() });
                }

                return ret;
            };
            return Wrike;
        })();
        forms.Wrike = Wrike;
    })(fpxt.forms || (fpxt.forms = {}));
    var forms = fpxt.forms;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=wrike.js.map
