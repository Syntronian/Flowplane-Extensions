/// <reference path="refs.d.ts" />

module fpxt {

    export class fpxtParam {
        constructor(public key: string, public value: string) { }
    }

    export class ExtensionDialog {

        public static initialise(extCode: string, authKeys: fpxtParam[], objParams: fpxtParam[]) {
            switch(extCode.toLowerCase()) {
                case forms.Asana.extId.toLowerCase():
                    var frm = new forms.Asana(authKeys, objParams);
                    break;
            }
        }

    }
}
