using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer;

namespace Extensions.Twitter
{
    public class Auth
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public Auth()
        {
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
            this.AccessToken = tokens.Token;
            this.AccessTokenSecret = tokens.TokenSecret;
            return new Dictionary<string, string>
                {
                    { "AccessToken", this.AccessToken },
                    { "AccessTokenSecret", this.AccessTokenSecret }
                };
        }
    }
}
