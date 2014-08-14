using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Formatting;
namespace FlowplaneExtensions.Models.api.OAuth
{
    public class Auth
    {
        public string GetLoginUrl(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
             var objParams = Common.TryGetParams(formData["objParams"]);

            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var redirectUri = objParams.FirstOrDefault(k => k.key == "redirectUri");
                return new Extensions.Podio.Auth(Common.GetAuthObject(authKeys)).GetLoginUrl(redirectUri.value);
            }
            throw new Exception("Invalid extension.");
        }

        public string GetAccessToken(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);

            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var accessCode = objParams.FirstOrDefault(k => k.key == "code");
                var redirectUri = objParams.FirstOrDefault(k => k.key == "redirectUri");
                return new Extensions.Podio.Auth(Common.GetAuthObject(authKeys)).GetAccessToken(accessCode.value, redirectUri.value);
            }
            throw new Exception("Invalid extension.");
        }
    }
}