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
        public string ActivateObject(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);

            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = authKeys.FirstOrDefault(k => k.key == "API_Key");
                if (apiKey == null) throw new Exception("Invalid API_Key");

                var name = Common.TryGetString(objParams.FirstOrDefault(k => k.key == "taskdesc"));
                var daysDue = Common.TryGetString(objParams.FirstOrDefault(k => k.key == "taskduedays"));
                var assigneeId = Common.TryGetString(objParams.FirstOrDefault(k => k.key == "taskassignee"));
                var workspace = Common.TryGetString(objParams.FirstOrDefault(k => k.key == "taskworkspace"));
                var project = Common.TryGetString(objParams.FirstOrDefault(k => k.key == "taskproject"));
                
                var task = new Extensions.Asana.Tasks(new Auth(apiKey.value));

                if (string.IsNullOrEmpty(daysDue))
                {
                    return task.Create(workspace, project, name, assigneeId, null).id;
                }

                var taskDaysDue = 0;
                if (!int.TryParse(daysDue, out taskDaysDue)) throw new Exception("Days due is not a valid integer.");

                return task.Create(workspace, project, name, assigneeId, taskDaysDue).id;
            }

            if (extId.Equals(new Extensions.Facebook.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var appId = formData["appId"];
                var appToken = formData["appToken"];
                var message = formData["message"];
                var linkUrl = formData["linkUrl"];
                var pictureUrl = formData["pictureUrl"];
                var name = formData["name"];
                var caption = formData["caption"];
                var description = formData["description"];
                var actionName = formData["actionName"];
                var actionLinkUrl = formData["actionLinkUrl"];

                if (string.IsNullOrEmpty(appId)) throw new Exception("Facebook app id is mandatory.");
                if (string.IsNullOrEmpty(appToken)) throw new Exception("Facebook app token is mandatory.");
                new Extensions.Facebook.Status(appToken, appId).UpdateStatus(message, linkUrl, pictureUrl, name, caption, description, actionName, actionLinkUrl);
                return System.Net.HttpStatusCode.OK.ToString();
            }

            if (extId.Equals(new Extensions.Twitter.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var consumerKey = formData["consumerKey"];
                var consumerSecret = formData["consumerSecret"];
                var accessToken = formData["accessToken"];
                var accessTokenSecret = formData["accessTokenSecret"];
                var message = formData["message"];

                if (string.IsNullOrEmpty(consumerKey)) throw new Exception("Twitter consumer key is mandatory.");
                if (string.IsNullOrEmpty(consumerSecret)) throw new Exception("Twitter consumer secret is mandatory.");
                if (string.IsNullOrEmpty(accessToken)) throw new Exception("Twitter access token is mandatory.");
                if (string.IsNullOrEmpty(accessTokenSecret)) throw new Exception("Twitter access token secret is mandatory.");
                new Extensions.Twitter.Tweets(consumerKey, consumerSecret, accessToken, accessTokenSecret).UpdateStatus(message);
                return System.Net.HttpStatusCode.OK.ToString();
            }

            throw new Exception("Invalid extension.");
        }



        public HttpResponseMessage CompleteActivity(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);

            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = formData["API_Key"];
                var completed = formData["completed"];
                var taskId = formData["taskId"];

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