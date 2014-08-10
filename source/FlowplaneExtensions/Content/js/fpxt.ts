/// <reference path="refs.d.ts" />

module fpxt {

    export class fpxtParam {
        constructor(public key: string, public value: string) { }
    }

    // Simple hack for now. Override this base url for debugging.
    export class BaseApiUrl {
        static _baseApiUrl: string = 'https://fpxt.azurewebsites.net/';

        public static get path(): string {
            var ret = BaseApiUrl._baseApiUrl;
            // ret = 'http://localhost/flowplaneextensions/';
            return ret;
        }
    }

    export class ExtensionDialog {
        
        public static initialise(extCode: string, authKeys: fpxtParam[], objParams: fpxtParam[]) {
            switch(extCode.toLowerCase()) {
                case forms.Asana.extId.toLowerCase():
                    forms.Asana.setup(BaseApiUrl.path, authKeys, objParams);
                    break;
            }
        }

        public static getProperties(extCode: string): fpxtParam[] {
            switch (extCode.toLowerCase()) {
                case forms.Asana.extId.toLowerCase():
                    return forms.Asana.getProperties();

                default:
                    return [];
            }
        }

    }
}
