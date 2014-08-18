/// <reference path="../refs.d.ts" />

module fpxt.forms {

    export class Facebook implements IForm {

        public extId: string = 'FACEBOOK';

        public setup(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }

        public setupAuth(authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            if ($('#txtFacebookAppId').val() != '' && $('#txtFacebookAppSecret').val() != '') {
                var bswitch: any = $('#chkFacebookOauthOption');
                bswitch.bootstrapSwitch('toggleState');
            }

            onCompleted();
        }

        public fill(baseApiUrl: string, authKeys: fpxtParam[], values: fpxtParam[]) {
            $("#txtEventParamFacebookStatus").val('');

            if (values == null) return;
            values.forEach((p) => {
                switch (p.key) {
                    case 'facebookstatus':
                        $("#txtEventParamFacebookStatus").val(p.value);
                        break;
                }
            });
        }

        public getProperties(): fpxtParam[] {
            if ($("#txtEventParamFacebookStatus").val() == "")
                throw "Facebook status is required.";

            var ret: fpxtParam[] = [];

            ret.push({ key: "facebookstatus", value: $("#txtEventParamFacebookStatus").val() });

            return ret;
        }
    }
}
