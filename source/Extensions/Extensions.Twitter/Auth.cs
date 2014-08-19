using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;
using Twitterizer;
namespace Extensions.Twitter
{
    public class Auth : IAuth
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AuthToken { get; set; }
        public string AuthTokenSecret { get; set; }

        public Auth()
        {
        }

        public Auth(string consumerKey,
                    string consumerSecret,
                    string authToken,
                    string authTokenSecret)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
            this.AuthToken = authToken;
            this.AuthTokenSecret = authTokenSecret;
        }

        public string GetLoginUrl(string callbackUrl)
        {
            return string.Format("https://twitter.com/oauth/authorize?oauth_token={0}", this.GetRequestToken(callbackUrl));
        }

        public string GetRequestToken(string callbackUrl)
        {
            var rs = OAuthUtility.GetRequestToken(this.ConsumerKey, this.ConsumerSecret, callbackUrl);
            return rs.Token;
        }

        public Dictionary<string, string> GetAccessTokens(string requestToken, string verifier)
        {
            var tokens = OAuthUtility.GetAccessToken(this.ConsumerKey, this.ConsumerSecret, requestToken, verifier);
            return new Dictionary<string, string>
                {
                    { "AccessToken", tokens.Token },
                    { "AccessTokenSecret", tokens.TokenSecret }
                };
        }
    }
}
