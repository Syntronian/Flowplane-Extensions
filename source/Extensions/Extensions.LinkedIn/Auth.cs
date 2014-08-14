using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.LinkedIn
{
    public static class Parameters
    {
        public const string RespType = "response_type";
        public const string ClientId = "client_id";
        public const string Scope = "scope";
        public const string State = "state";
        public const string CallbackUrl = "redirect_uri";

        public const string GrantType = "grant_type";
        public const string Code = "code";
        public const string ClientSecret = "client_secret";
        public const string Auth2AccessToken = "oauth2_access_token";
    }
    public class Auth : IAuth
    {
        string linkedScope = "r_basicprofile%20rw_nus";//"r_fullprofile%20r_emailaddress%20r_network%20rw_nus";
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string CallbackUrl { get; set; }
        public string LinkedInScope { get { return linkedScope; } }
        public string AuthToken { get; set; }
        public string AuthTokenSecret { get; set; }
        public Auth(Auth auth)
        {
            this.CallbackUrl = auth.CallbackUrl;
            this.ConsumerKey = auth.ConsumerKey;
            this.ConsumerSecret = auth.ConsumerSecret;
            this.AuthToken = auth.AuthToken;
        }

        public Auth(string key, string secret,string accessToken,string cb_url)
        {
            this.CallbackUrl = cb_url;
            this.ConsumerKey = key;
            this.ConsumerSecret = secret;
            this.AuthToken = accessToken;
        }

        //public string GenerateUrl(string apiMethod)
        //{
        //    StringBuilder QueryStringRequest = new StringBuilder();
        //    QueryStringRequest.AppendFormat("{0}", apiMethod);
        //    QueryStringRequest.AppendFormat("?{0}={1}", Parameters.RespType, "code");
        //    QueryStringRequest.AppendFormat("&{0}={1}", Parameters.ClientId, this.ApiKey);
        //    QueryStringRequest.AppendFormat("&{0}={1}", Parameters.Scope, linkedScope);
        //    QueryStringRequest.AppendFormat("&{0}={1}", Parameters.State, this.GetState());
        //    QueryStringRequest.AppendFormat("&{0}={1}", Parameters.CallbackUrl, this.callbackUrl);

        //    return QueryStringRequest.ToString();
        //}

        public string GetState()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 13; i++)
            {
                builder.Append(Convert.ToChar(
                        Convert.ToInt32(Math.Floor(
                                26 * random.NextDouble() + 65))));
            }
            return builder.ToString();
        }

    }
    public class ResponseObject
    {
        public string expires_in { get; set; }
        public string access_token { get; set; }
    }
}
