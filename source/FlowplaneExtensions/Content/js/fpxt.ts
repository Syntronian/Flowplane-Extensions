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
        
        private static getExtension(extCode: string): forms.IForm {
            var exts: forms.IForm[] = [];

            // register extensions here
            exts.push(new forms.Asana());
            exts.push(new forms.Paymo());
            exts.push(new forms.Podio());

            return Enumerable.from(exts).firstOrDefault(x => { return x.extId.toLowerCase() == extCode.toLowerCase(); });
        }

        public static initialise(extCode: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void) {
            ExtensionDialog.getExtension(extCode).setup(BaseApiUrl.path, authKeys, objParams, onCompleted);
        }

        public static fill(extCode: string, authKeys: fpxtParam[], values: fpxtParam[]) {
            ExtensionDialog.getExtension(extCode).fill(BaseApiUrl.path, authKeys, values);
        }

        public static getProperties(extCode: string): fpxtParam[] {
            return ExtensionDialog.getExtension(extCode).getProperties();
        }

    }
}
