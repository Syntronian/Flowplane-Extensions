using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using ExtensionsCore;
namespace FlowplaneExtensions.Models.api
{
    public class Common
    {
        public static string FlowplaneDotCom
        {
            get
            {
                return "https://flowplane.com";
            }
        }

        internal static string GetValue(FormDataCollection data, string key)
        {
            return data.FirstOrDefault(a => a.Key == key).Value ?? "";
        }

        internal static int? TryGetInt(FpxtParam value)
        {
            if (value == null) return null;
            if (string.IsNullOrEmpty(value.value)) return null;

            int? ret = null;
            var tmp = 0;
            if (Int32.TryParse(value.value, out tmp)) ret = tmp;
            return ret;
        }

        internal static bool? TryGetBool(FpxtParam value)
        {
            if (value == null) return null;
            if (string.IsNullOrEmpty(value.value)) return null;

            bool? ret = null;
            var tmp = false;
            if (bool.TryParse(value.value, out tmp)) ret = tmp;
            return ret;
        }

        internal static string TryGetString(FpxtParam value)
        {
            return value == null ? null : value.value;
        }

        internal static List<FpxtParam> TryGetParams(string data)
        {
            if (String.IsNullOrEmpty(data)) return new List<FpxtParam>();
            return JsonConvert.DeserializeObject<List<FpxtParam>>(data);
        }

        internal static Extensions.Podio.Auth GetAuthObject(List<FpxtParam> authKeys)
        {
            var clientId = authKeys.FirstOrDefault(k => k.key == "clientId");
            if (clientId == null) throw new Exception("Invalid clientId");

            var clientSecret = authKeys.FirstOrDefault(k => k.key == "clientSecret");
            if (clientSecret == null) throw new Exception("Invalid clientSecret");

            var accessToken = authKeys.FirstOrDefault(k => k.key == "accessToken");
            if (accessToken == null) throw new Exception("Invalid accessToken");

            return new Extensions.Podio.Auth(
                    clientId.value,
                    clientSecret.value,
                    accessToken.value,
                    Common.TryGetInt(authKeys.FirstOrDefault(k => k.key == "organisationId")));
        }

        internal static IAuth GetAuthObject(string extId, List<FpxtParam> authKeys, List<FpxtParam> objParams = null)
        {
            var consumerKey = authKeys.FirstOrDefault(k => k.key == "consumerKey");
            if (consumerKey == null) throw new Exception("Invalid consumerKey");

            var consumerSecret = authKeys.FirstOrDefault(k => k.key == "consumerSecret");
            if (consumerSecret == null) throw new Exception("Invalid consumerSecret");

            var accessToken = authKeys.FirstOrDefault(k => k.key == "accessToken");
            if (accessToken == null) throw new Exception("Invalid accessToken");


            if (extId.Equals(new Extensions.Wrike.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var accessTokenSecret = authKeys.FirstOrDefault(k => k.key == "accessTokenSecret");
                if (accessTokenSecret == null) throw new Exception("Invalid accessTokenSecret");

                var callback = authKeys.FirstOrDefault(k => k.key == "callback");
                if (callback == null) throw new Exception("Invalid callback");

                return new Extensions.Wrike.Auth(consumerKey.value, consumerSecret.value, accessToken.value, accessTokenSecret.value, callback.value);
            }

            if (extId.Equals(new Extensions.LinkedIn.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var callback = authKeys.FirstOrDefault(k => k.key == "callback");
                if (callback == null) throw new Exception("Invalid callback");

                return new Extensions.LinkedIn.Auth(consumerKey.value, consumerSecret.value, accessToken.value, callback.value);
            }

            if (extId.Equals(new Extensions.Twitter.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var accessTokenSecret = authKeys.FirstOrDefault(k => k.key == "accessTokenSecret");
                if (accessTokenSecret == null) throw new Exception("Invalid accessTokenSecret");

                return new Extensions.Twitter.Auth(consumerKey.value, consumerSecret.value, accessToken.value, accessTokenSecret.value);
            }
            throw new Exception("Invalid extension.");
        }
    }
}