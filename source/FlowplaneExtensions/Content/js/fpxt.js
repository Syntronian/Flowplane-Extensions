/// <reference path="refs.d.ts" />
var fpxt;
(function (fpxt) {
    var fpxtParam = (function () {
        function fpxtParam(key, value) {
            this.key = key;
            this.value = value;
        }
        return fpxtParam;
    })();
    fpxt.fpxtParam = fpxtParam;

    var ExtensionDialog = (function () {
        function ExtensionDialog() {
        }
        ExtensionDialog.initialise = function (extCode, authKeys, objParams) {
            // Simple hack for now. Override this base url for debugging.
            var baseApiUrl = 'https://fpxt.azurewebsites.net/';

            switch (extCode.toLowerCase()) {
                case fpxt.forms.Asana.extId.toLowerCase():
                    var frm = new fpxt.forms.Asana(baseApiUrl, authKeys, objParams);
                    break;
            }
        };
        return ExtensionDialog;
    })();
    fpxt.ExtensionDialog = ExtensionDialog;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=fpxt.js.map
