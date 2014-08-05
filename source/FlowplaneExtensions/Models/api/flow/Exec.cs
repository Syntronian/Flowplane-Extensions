using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
namespace FlowplaneExtensions.Models.api.flow
{
    public class Exec
    {
        public HttpResponseMessage PostUpdate(string extId,string message,string appId,string appToken, string linkUrl, string pictureUrl, string name,string caption, string description, string actionName,string actionLinkUrl)
        {
            if (extId.Equals(new Extensions.Facebook.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                new Extensions.Facebook.Status(appToken, appId).UpdateStatus(message, linkUrl, pictureUrl, name, caption, description, actionName, actionLinkUrl);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            throw new Exception("Invalid extension.");
        }
    }
}