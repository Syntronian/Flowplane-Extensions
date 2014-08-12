using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;

namespace Extensions.Twitter 
{
    public class Tweets
    {
        private TwitterContext twitterCtx;

        public Tweets(Auth auth) : this(auth.ConsumerKey,auth.ConsumerSecret,auth.AuthToken,auth.AuthTokenSecret)
        {}

        public Tweets(string consumerKey,
                      string consumerSecret,
                      string accessToken,
                      string accessTokenSecret)
        {
            var auth = new SingleUserAuthorizer
            {
                Credentials = new InMemoryCredentials
                {
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret,
                    OAuthToken = accessToken,
                    AccessToken = accessTokenSecret
                }
            };
            this.twitterCtx = new TwitterContext(auth);
        }

        public void UpdateStatus(string status)
        {
            var rs = this.twitterCtx.UpdateStatus(status);
        }
    }
}
