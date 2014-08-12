using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer;
using ExtensionsCore;
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

        public string GetRequestToken(string consumerKey,
                                      string consumerSecret,
                                      string callbackUrl)
        {
            if (!string.IsNullOrEmpty(consumerKey) && !string.IsNullOrEmpty(consumerSecret))
            {
                this.ConsumerKey = consumerKey;
                this.ConsumerSecret = consumerSecret;
            }
            var rs = OAuthUtility.GetRequestToken(this.ConsumerKey, this.ConsumerSecret, callbackUrl);
            return rs.Token;
        }

        public Dictionary<string, string> GetAccessToken(string consumerKey,
                                                         string consumerSecret,
                                                         string requestToken,
                                                         string verifier)
        {
            if (!string.IsNullOrEmpty(consumerKey) && !string.IsNullOrEmpty(consumerSecret))
            {
                this.ConsumerKey = consumerKey;
                this.ConsumerSecret = consumerSecret;
            }
            var tokens = OAuthUtility.GetAccessToken(this.ConsumerKey, this.ConsumerSecret, requestToken, verifier);
            this.AuthToken = tokens.Token;
            this.AuthTokenSecret = tokens.TokenSecret;
            return new Dictionary<string, string>
                {
                    { "AccessToken", this.AuthToken },
                    { "AccessTokenSecret", this.AuthTokenSecret }
                };
        }
    }
}
