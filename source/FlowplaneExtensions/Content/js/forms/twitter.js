/// <reference path="../refs.d.ts" />
var fpxt;
(function (fpxt) {
    (function (forms) {
        var Twitter = (function () {
            function Twitter() {
                this.extId = 'TWITTER';
            }
            Twitter.prototype.setup = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            Twitter.prototype.setupAuthPre = function (baseApiUrl, authKeys, objParams, onCompleted) {
                onCompleted();
            };

            Twitter.prototype.setupAuthPost = function (baseApiUrl, authKeys, objParams, onCompleted) {
                if ($('#txtTwitterConsumerKey').val() != '' && $('#txtTwitterConsumerSecret').val() != '') {
                    var bswitch = $('#chkTwitterOauthOption');
                    bswitch.bootstrapSwitch('toggleState');
                }

                onCompleted();
            };

            Twitter.prototype.fill = function (baseApiUrl, authKeys, values) {
                $("#txtEventParamTweetStatus").val('');

                if (values == null)
                    return;
                values.forEach(function (p) {
                    switch (p.key) {
                        case 'tweetstatus':
                            $("#txtEventParamTweetStatus").val(p.value);
                            break;
                    }
                });
            };

            Twitter.prototype.getProperties = function () {
                if ($("#txtEventParamTweetStatus").val() == "")
                    throw "Tweet status is required.";

                var ret = [];

                ret.push({ key: "tweetstatus", value: $("#txtEventParamTweetStatus").val() });

                return ret;
            };
            return Twitter;
        })();
        forms.Twitter = Twitter;
    })(fpxt.forms || (fpxt.forms = {}));
    var forms = fpxt.forms;
})(fpxt || (fpxt = {}));
//# sourceMappingURL=twitter.js.map
