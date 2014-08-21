/// <reference path="../refs.d.ts" />
var fpxt;
(function (fpxt) {
    (function (forms) {
        var LinkedIn = (function () {
            function LinkedIn() {
                this.extId = 'LINKEDIN';
            }
            LinkedIn.prototype.setup = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            LinkedIn.prototype.setupAuthPre = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            LinkedIn.prototype.setupAuthPost = function (baseApiUrl, authKeys, objParams, onCompleted) {
                if ($('#txtLinkedInApiKey').val() != '' && $('#txtLinkedInApiSecret').val() != '') {
                    var bswitch = $('#chkLinkedInOauthOption');
                    bswitch.bootstrapSwitch('toggleState');
                }

                onCompleted();
            };

            LinkedIn.prototype.fill = function (baseApiUrl, authKeys, values) {
                $("#txtEventParamShareStatus").val('');

                if (values == null)
                    return;
                values.forEach(function (p) {
                    switch (p.key) {
                        case 'linkedinsharestatus':
                            $("#txtEventParamShareStatus").val(p.value);
                            break;
                    }
                });
            };

            LinkedIn.prototype.getProperties = function () {
                if ($("#txtEventParamShareStatus").val() == "")
                    throw "share message is required.";

                var ret = [];

                ret.push({ key: "linkedinsharestatus", value: $("#txtEventParamShareStatus").val() });

                return ret;
            };
            return LinkedIn;
        })();
        forms.LinkedIn = LinkedIn;
    })(fpxt.forms || (fpxt.forms = {}));
    var forms = fpxt.forms;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=linkedin.js.map
