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
            switch (extCode.toLowerCase()) {
                case fpxt.forms.Asana.extId.toLowerCase():
                    var frm = new fpxt.forms.Asana(authKeys, objParams);
                    break;
            }
        };
        return ExtensionDialog;
    })();
    fpxt.ExtensionDialog = ExtensionDialog;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=fpxt.js.map
