/// <reference path="../refs.d.ts" />

module fpxt.forms {

    export class Wrike implements IForm {

        public extId: string = 'WRIKE';
        private folders: any[];
        private selectedFolders: string[];
        public setup(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            $("#assignees-loading").show();
            $("#cboActivityParamAssignee").hide();

            // get data first
            var pd = new Array();
            pd.push(new shearnie.tools.PostData(baseApiUrl + 'api/process/getassignees', { extId: this.extId, authKeys: JSON.stringify(authKeys), objParams: JSON.stringify(objParams) }));

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

                onCompleted();
            });
        }

        load_folders(checkedNodes: string[]) {
            $("#folders-loading").show();
            $("#treeActivityParamFolders").hide();

            this.selectedFolders = [];
            var tree: any = $('#treeActivityParamFolders');

            tree.on('changed.jstree', (e, data) => {
                if (data.action == 'select_node') {
                    if (!Enumerable.from(this.selectedFolders).any((i) => i == data.node.id))
                        this.selectedFolders.push(data.node.id);
                } else if (data.action == 'deselect_node') {
                    if (Enumerable.from(this.selectedFolders).any((i) => i == data.node.id))
                        this.selectedFolders.splice(this.selectedFolders.indexOf(data.node.id), 1);
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
                checkedNodes.forEach((i) => {
                    $.jstree.reference(i).select_node(i, true);
                });
                this.selectedFolders = checkedNodes;
            }

            $("#folders-loading").hide();
            $("#treeActivityParamFolders").show();
        }

        getKids(parentId: string): any[] {
            var ret: any[] = [];

            Enumerable.from(this.folders).where((i) => i.parentId == parentId && i.id != parentId).forEach((i) => {
                ret.push({
                    "id": i.id,
                    "text": i.title,
                    "children": this.getKids(i.id)
                });
            });

            return ret;
        }

        public setupAuthPre(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }

        public setupAuthPost(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
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

       
        public getProperties(): fpxtParam[]{
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
 