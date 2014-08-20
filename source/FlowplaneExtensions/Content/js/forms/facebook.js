/// <reference path="../refs.d.ts" />
var fpxt;
(function (fpxt) {
    (function (forms) {
        var Facebook = (function () {
            function Facebook() {
                this.extId = 'FACEBOOK';
            }
            Facebook.prototype.setup = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            Facebook.prototype.setupAuthPre = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            Facebook.prototype.setupAuthPost = function (baseApiUrl, authKeys, objParams, onCompleted) {
                if ($('#txtFacebookAppId').val() != '' && $('#txtFacebookAppSecret').val() != '') {
                    var bswitch = $('#chkFacebookOauthOption');
                    bswitch.bootstrapSwitch('toggleState');
                }

                onCompleted();
            };

            Facebook.prototype.fill = function (baseApiUrl, authKeys, values) {
                $("#txtEventParamFacebookStatus").val('');

                if (values == null)
                    return;
                values.forEach(function (p) {
                    switch (p.key) {
                        case 'facebookstatus':
                            $("#txtEventParamFacebookStatus").val(p.value);
                            break;
                    }
                });
            };

            Facebook.prototype.getProperties = function () {
                if ($("#txtEventParamFacebookStatus").val() == "")
                    throw "Facebook status is required.";

                var ret = [];

                ret.push({ key: "facebookstatus", value: $("#txtEventParamFacebookStatus").val() });

                return ret;
            };
            return Facebook;
        })();
        forms.Facebook = Facebook;
    })(fpxt.forms || (fpxt.forms = {}));
    var forms = fpxt.forms;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=facebook.js.map
