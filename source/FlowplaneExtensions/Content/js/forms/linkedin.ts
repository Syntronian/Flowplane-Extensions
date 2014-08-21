/// <reference path="../refs.d.ts" />

module fpxt.forms {

    export class LinkedIn implements IForm {

        public extId: string = 'LINKEDIN';

        public setup(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }
        
        public setupAuthPre(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }

        public setupAuthPost(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            if ($('#txtLinkedInApiKey').val() != '' && $('#txtLinkedInApiSecret').val() != '') {
                var bswitch: any = $('#chkLinkedInOauthOption');
                bswitch.bootstrapSwitch('toggleState');
            }

            onCompleted();
        }

        public fill(baseApiUrl: string, authKeys: fpxtParam[], values: fpxtParam[]) {
            $("#txtEventParamShareStatus").val('');

            if (values == null) return;
            values.forEach((p) => {
                switch (p.key) {
                    case 'linkedinsharestatus':
                        $("#txtEventParamShareStatus").val(p.value);
                        break;
                }
            });
        }

        public getProperties(): fpxtParam[] {
            if ($("#txtEventParamShareStatus").val() == "")
                throw "share message is required.";

            var ret: fpxtParam[] = [];

            ret.push({ key: "linkedinsharestatus", value: $("#txtEventParamShareStatus").val() });

            return ret;
        }
    }
}
