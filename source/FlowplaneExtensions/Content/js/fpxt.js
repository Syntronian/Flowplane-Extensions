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

                //ret = 'http://localhost/flowplaneextensions/';
                return ret;
            },
            enumerable: true,
            configurable: true
        });

        Object.defineProperty(BaseApiUrl, "corepath", {
            get: function () {
                var ret = 'https://flowplane.com/';

                //ret = 'http://localhost/flowplane/';
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
        ExtensionDialog.getExtension = function (extCode) {
            var exts = [];

            // register extensions here
            exts.push(new fpxt.forms.Asana());
            exts.push(new fpxt.forms.Facebook());
            exts.push(new fpxt.forms.Paymo());
            exts.push(new fpxt.forms.Podio());
            exts.push(new fpxt.forms.Twitter());
            exts.push(new fpxt.forms.LinkedIn());

            return Enumerable.from(exts).firstOrDefault(function (x) {
                return x.extId.toLowerCase() == extCode.toLowerCase();
            });
        };

        ExtensionDialog.initialise = function (extCode, authKeys, objParams, onCompleted) {
            ExtensionDialog.getExtension(extCode).setup(BaseApiUrl.path, authKeys, objParams, onCompleted);
        };

        ExtensionDialog.initialiseAuthPre = function (extCode, authKeys, objParams, onCompleted) {
            ExtensionDialog.getExtension(extCode).setupAuthPre(BaseApiUrl.path, authKeys, objParams, onCompleted);
        };

        ExtensionDialog.initialiseAuthPost = function (extCode, authKeys, objParams, onCompleted) {
            ExtensionDialog.getExtension(extCode).setupAuthPost(BaseApiUrl.path, authKeys, objParams, onCompleted);
        };

        ExtensionDialog.fill = function (extCode, authKeys, values) {
            ExtensionDialog.getExtension(extCode).fill(BaseApiUrl.path, authKeys, values);
        };

        ExtensionDialog.getProperties = function (extCode) {
            return ExtensionDialog.getExtension(extCode).getProperties();
        };
        return ExtensionDialog;
    })();
    fpxt.ExtensionDialog = ExtensionDialog;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=fpxt.js.map
