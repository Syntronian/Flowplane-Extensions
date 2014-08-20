/// <reference path="../refs.d.ts" />

module fpxt.forms {

    export class Twitter implements IForm {

        public extId: string = 'TWITTER';

        public setup(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }
        
        public setupAuthPre(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            onCompleted();
        }

        public setupAuthPost(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            if ($('#txtTwitterConsumerKey').val() != '' && $('#txtTwitterConsumerSecret').val() != '') {
                var bswitch: any = $('#chkTwitterOauthOption');
                bswitch.bootstrapSwitch('toggleState');
            }

            onCompleted();
        }

        public fill(baseApiUrl: string, authKeys: fpxtParam[], values: fpxtParam[]) {
            $("#txtEventParamTweetStatus").val('');

            if (values == null) return;
            values.forEach((p) => {
                switch (p.key) {
                    case 'tweetstatus':
                        $("#txtEventParamTweetStatus").val(p.value);
                        break;
                }
            });
        }

        public getProperties(): fpxtParam[] {
            if ($("#txtEventParamTweetStatus").val() == "")
                throw "Tweet status is required.";

            var ret: fpxtParam[] = [];

            ret.push({ key: "tweetstatus", value: $("#txtEventParamTweetStatus").val() });

            return ret;
        }
    }
}
