using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;

namespace Extensions.Facebook
{
    public class Auth
    {
        public string AccessToken { get; set; }

        public string GetLoginUrl(string appId, string appSecret, string absoluteUri)
        {
            var fbc = new FacebookClient()
            {
                AppId = appId,
                AppSecret = appSecret
            };

            var authUrl = fbc.GetLoginUrl(new
            {
                scope = "publish_stream",
                redirect_uri = absoluteUri
            });

            return authUrl.AbsoluteUri;
        }

        public string GetAccessToken(string appId, string appSecret, string code, string absoluteUri)
        {
            var fbc = new FacebookClient()
            {
                AppId = appId,
                AppSecret = appSecret
            };

            dynamic result = fbc.Get("oauth/access_token", new
            {
                client_id = appId,
                client_secret = appSecret,
                grant_type = "authorization_code",
                redirect_uri = absoluteUri,
                code = code
            });

            this.AccessToken = result.access_token;

            return result.access_token;
        }
    }
}
