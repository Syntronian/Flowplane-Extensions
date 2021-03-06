﻿/// <reference path="../refs.d.ts" />
var fpxt;
(function (fpxt) {
    (function (forms) {
        var Wrike = (function () {
            function Wrike() {
                this.extId = 'WRIKE';
            }
            Wrike.prototype.setup = function (baseApiUrl, authKeys, objParams, onCompleted) {
                var _this = this;
                $("#assignees-loading").show();
                $("folders-loading").show();
                $("#cboActivityParamAssignee").hide();
                $("#treeActivityParamFolders").hide();

                // get data first
                var pd = new Array();
                pd.push(new shearnie.tools.PostData(fpxt.BaseApiUrl.corepath + 'api/oauth/getwrikeassignees', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));
                pd.push(new shearnie.tools.PostData(fpxt.BaseApiUrl.corepath + 'api/oauth/getwrikefolders', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

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

                    _this.folders = pd[1].result.tree.foldersTree.folders;

                    //fill folders
                    shearnie.tools.html.fillTree($("#treeActivityParamFolders"), _this.folders, null); // this.onTreeChange);
                    $("#folders-loading").hide();
                    $("#treeActivityParamFolders").show();

                    onCompleted();
                });
            };

            //onTreeChange(nodes: string[])
            //{
            //    this.selectedFolders = nodes;
            //}
            Wrike.prototype.load_folders = function (checkedNodes) {
                $("#folders-loading").show();
                $("#treeActivityParamFolders").hide();
                this.selectedFolders = [];
                shearnie.tools.html.fillTree($("#treeActivityParamFolders"), this.folders, checkedNodes); //, this.onTreeChange);

                $("#folders-loading").hide();
                $("#treeActivityParamFolders").show();
            };

            Wrike.prototype.setupAuthPre = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            Wrike.prototype.setupAuthPost = function (baseApiUrl, authKeys, objParams, onCompleted) {
                if ($('#txtWrikeConsumerKey').val() != '' && $('#txtWrikeConsumerSecret').val() != '') {
                    var bswitch = $('#chkWrikeOauthOption');
                    bswitch.bootstrapSwitch('toggleState');
                }
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

            Wrike.prototype.get_checkednodes = function () {
                var checked_ids = [];
                var checked = $.jstree.reference("#treeActivityParamFolders").get_selected();
                checked.forEach(function (i) {
                    checked_ids.push(i);
                });
                return checked_ids;
            };

            Wrike.prototype.getProperties = function () {
                if ($("#txtActivityParamTaskDesc").val() == "")
                    throw "Description is required.";

                var ret = [];

                ret.push({ key: "taskdesc", value: $("#txtActivityParamTaskDesc").val() });

                if ($("#cboActivityParamAssignee").val()) {
                    ret.push({ key: "taskassignee", value: $("#cboActivityParamAssignee").val() });
                    ret.push({ key: "taskassigneename", value: $("#cboActivityParamAssignee option:selected").text() });
                }
                this.selectedFolders = this.get_checkednodes();
                ret.push({ key: "taskfolders", value: JSON.stringify(this.selectedFolders) });

                return ret;
            };
            return Wrike;
        })();
        forms.Wrike = Wrike;
    })(fpxt.forms || (fpxt.forms = {}));
    var forms = fpxt.forms;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=wrike.js.map
