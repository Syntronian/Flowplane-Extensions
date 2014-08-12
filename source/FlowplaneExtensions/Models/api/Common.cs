using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace FlowplaneExtensions.Models.api
{
    public class Common
    {
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
    }
}