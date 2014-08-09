using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Net.Http;
using Extensions.Asana;

namespace FlowplaneExtensions.Models.api.flow
{
    public class Exec
    {
        public string ActivateObject(string extId,FormDataCollection data)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = data["apiKey"];
                var daysDue = data["daysDue"];
                var workspace = data["workspace"];
                var project = data["project"];
                var name = data["name"];
                var assigneeId = data["assigneeId"];
                
                var task = new Extensions.Asana.Tasks(new Auth(apiKey));

                if (string.IsNullOrEmpty(daysDue))
                {
                    return task.Create(workspace, project, name, assigneeId, null).id;
                }

                var taskDaysDue = 0;
                if (!int.TryParse(daysDue, out taskDaysDue)) throw new Exception("daysDue is not a valid integer.");

                return task.Create(workspace, project, name, assigneeId, taskDaysDue).id;
            }

            if (extId.Equals(new Extensions.Facebook.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var appId = data["appId"];
                var appToken = data["appToken"];
                var message = data["message"];
                var linkUrl = data["linkUrl"];
                var pictureUrl = data["pictureUrl"];
                var name = data["name"];
                var caption = data["caption"];
                var description = data["description"];
                var actionName = data["actionName"];
                var actionLinkUrl = data["actionLinkUrl"];

                if (string.IsNullOrEmpty(appId)) throw new Exception("Facebook app id is mandatory.");
                if (string.IsNullOrEmpty(appToken)) throw new Exception("Facebook app token is mandatory.");
                new Extensions.Facebook.Status(appToken, appId).UpdateStatus(message, linkUrl, pictureUrl, name, caption, description, actionName, actionLinkUrl);
                return System.Net.HttpStatusCode.OK.ToString();
            }

            if (extId.Equals(new Extensions.Twitter.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var consumerKey = data["consumerKey"];
                var consumerSecret = data["consumerSecret"];
                var accessToken = data["accessToken"];
                var accessTokenSecret = data["accessTokenSecret"];
                var message = data["message"];

                if (string.IsNullOrEmpty(consumerKey)) throw new Exception("Twitter consumer key is mandatory.");
                if (string.IsNullOrEmpty(consumerSecret)) throw new Exception("Twitter consumer secret is mandatory.");
                if (string.IsNullOrEmpty(accessToken)) throw new Exception("Twitter access token is mandatory.");
                if (string.IsNullOrEmpty(accessTokenSecret)) throw new Exception("Twitter access token secret is mandatory.");
                new Extensions.Twitter.Tweets(consumerKey, consumerSecret, accessToken, accessTokenSecret).UpdateStatus(message);
                return System.Net.HttpStatusCode.OK.ToString();
            }

            throw new Exception("Invalid extension.");
        }
        


        public HttpResponseMessage CompleteActivity(string extId, FormDataCollection data)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = data["apiKey"];
                var completed = data["completed"];
                var taskId = data["taskId"];

                var task = new Extensions.Asana.Tasks(new Auth(apiKey));

                if (string.IsNullOrEmpty(completed))
                {
                    task.Update(taskId, null);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }

                var completeTask = false;
                if (!bool.TryParse(completed, out completeTask))
                    throw new Exception("completed must be true or false.");

                task.Update(taskId, completeTask);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }

            throw new Exception("Invalid extension.");
        }
    }
}