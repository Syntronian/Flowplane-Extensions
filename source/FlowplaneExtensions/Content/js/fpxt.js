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

    // Simple hack for now. Override this base url for debugging.
    var BaseApiUrl = (function () {
        function BaseApiUrl() {
        }
        Object.defineProperty(BaseApiUrl, "path", {
            get: function () {
                var ret = BaseApiUrl._baseApiUrl;

                // ret = 'http://localhost/flowplaneextensions/';
                return ret;
            },
            enumerable: true,
            configurable: true
        });
        BaseApiUrl._baseApiUrl = 'https://fpxt.azurewebsites.net/';
        return BaseApiUrl;
    })();
    fpxt.BaseApiUrl = BaseApiUrl;

    var ExtensionDialog = (function () {
        function ExtensionDialog() {
        }
        ExtensionDialog.initialise = function (extCode, authKeys, objParams) {
            switch (extCode.toLowerCase()) {
                case fpxt.forms.Asana.extId.toLowerCase():
                    fpxt.forms.Asana.setup(BaseApiUrl.path, authKeys, objParams);
                    break;
            }
        };

        ExtensionDialog.getProperties = function (extCode) {
            switch (extCode.toLowerCase()) {
                case fpxt.forms.Asana.extId.toLowerCase():
                    return fpxt.forms.Asana.getProperties();

                default:
                    return [];
            }
        };
        return ExtensionDialog;
    })();
    fpxt.ExtensionDialog = ExtensionDialog;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=fpxt.js.map
