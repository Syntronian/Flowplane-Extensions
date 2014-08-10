/// <reference path="refs.d.ts" />

module fpxt {

    export class fpxtParam {
        constructor(public key: string, public value: string) { }
    }

    export class ExtensionDialog {

        public static initialise(extCode: string, authKeys: fpxtParam[], objParams: fpxtParam[]) {
            // Simple hack for now. Override this base url for debugging.

            var baseApiUrl = 'https://fpxt.azurewebsites.net';


            
            switch(extCode.toLowerCase()) {
                case forms.Asana.extId.toLowerCase():
                    var frm = new forms.Asana(baseApiUrl, authKeys, objParams);
                    break;
            }
        }

    }
}
