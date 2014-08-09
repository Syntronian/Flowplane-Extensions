using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
namespace FlowplaneExtensions.Models.api.flow
{
    public class Exec
    {
        public HttpResponseMessage PostUpdate(string extId,
                                              string message,
                                              string linkUrl,
                                              string pictureUrl,
                                              string name,
                                              string caption,
                                              string description,
                                              string actionName,
                                              string actionLinkUrl,
                                              string appId = null,
                                              string appToken = null,
                                              string consumerKey=null,
                                              string consumerSecret = null,
                                              string accessToken = null,
                                              string accessTokenSecret = null)
        {
            if (extId.Equals(new Extensions.Facebook.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                if (appId == null) throw new Exception("Facebook app id is mandatory.");
                if (appToken == null) throw new Exception("Facebook app token is mandatory.");
                new Extensions.Facebook.Status(appToken, appId).UpdateStatus(message, linkUrl, pictureUrl, name, caption, description, actionName, actionLinkUrl);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }

            if (extId.Equals(new Extensions.Twitter.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                if (consumerKey == null) throw new Exception("Twitter consumer key is mandatory.");
                if (consumerSecret == null) throw new Exception("Twitter consumer secret is mandatory.");
                if (accessToken == null) throw new Exception("Twitter access token is mandatory.");
                if (accessTokenSecret == null) throw new Exception("Twitter access token secret is mandatory.");
                new Extensions.Twitter.Tweets(consumerKey,consumerSecret,accessToken,accessTokenSecret).UpdateStatus(message);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }

            throw new Exception("Invalid extension.");
        }
    }
}