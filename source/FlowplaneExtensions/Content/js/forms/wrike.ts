/// <reference path="../refs.d.ts" />

module fpxt.forms {

    export class Wrike implements IForm {

        public extId: string = 'WRIKE';
        private selectedFolders: string[];
        private folders: any[];
        public setup(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            $("#assignees-loading").show();
            $("folders-loading").show();
            $("#cboActivityParamAssignee").hide();
            $("#treeActivityParamFolders").hide();
            // get data first

            objParams.push({ key: "callback", value: baseApiUrl + 'Wrike/oauth' });

            var pd = new Array();
            pd.push(new shearnie.tools.PostData(fpxt.BaseApiUrl.corepath + 'api/oauth/getwrikeassignees', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));
            pd.push(new shearnie.tools.PostData(fpxt.BaseApiUrl.corepath + 'api/oauth/getwrikefolders', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

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

                //var td: shearnie.tools.html.treeNode;
                //td.value = pd[1].result;
                //this.folders = pd[1].result;

                //fill folders
                shearnie.tools.html.fillTree($("#treeActivityParamFolders"), pd[1].result.tree.foldersTree.folders, null);
                $("#folders-loading").hide();
                $("#treeActivityParamFolders").show();

                onCompleted();
            });
        }

        load_folders(checkedNodes: string[]) {
            $("#folders-loading").show();
            $("#treeActivityParamFolders").hide();

            // shearnie.tools.html.fillTree($("#treeActivityParamFolders"), checkedNodes);

            $("#folders-loading").hide();
            $("#treeActivityParamFolders").show();
        }

        public setupAuthPre(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }

        public setupAuthPost(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            if ($('#txtWrikeConsumerKey').val() != '' && $('#txtWrikeConsumerSecret').val() != '') {
                var bswitch: any = $('#chkWrikeOauthOption');
                bswitch.bootstrapSwitch('toggleState');
            }
            onCompleted();
        }

        public fill(baseApiUrl: string, authKeys: fpxtParam[], values: fpxtParam[]) {
            $("#txtActivityParamTaskDesc").val('');
            $("#cboActivityParamAssignee").val(null);

            if (values == null) return;
            values.forEach((p) => {
                switch (p.key) {
                    case 'taskdesc':
                        $("#txtActivityParamTaskDesc").val(p.value);
                        break;
                    case 'taskfolders':
                        this.selectedFolders = JSON.parse(p.value);
                        break;
                    case 'taskassignee':
                        $("#cboActivityParamAssignee").val(p.value);
                        break;
                }
            });

            this.load_folders(this.selectedFolders);
        }


        public getProperties(): fpxtParam[] {
            if ($("#txtActivityParamTaskDesc").val() == "")
                throw "Description is required.";

            var ret: fpxtParam[] = [];
            if ($("#cboActivityParamAssignee").val()) {
                ret.push({ key: "taskassignee", value: $("#cboActivityParamAssignee").val() });
                ret.push({ key: "taskassigneename", value: $("#cboActivityParamAssignee option:selected").text() });
            }

            return ret;
        }
    }
}
