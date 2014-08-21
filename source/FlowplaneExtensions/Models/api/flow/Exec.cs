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
                var accessToken = authKeys.FirstOrDefault(k => k.key == "AccessToken");
                if (accessToken == null) throw new Exception("Facebook app token is mandatory.");

                new Extensions.Facebook.Status(accessToken.value).UpdateStatus(Common.TryGetString(objParams.FirstOrDefault(k => k.key == "facebookstatus")));
                return System.Net.HttpStatusCode.OK.ToString();
            }

            if (extId.Equals(new Extensions.Twitter.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                new Extensions.Twitter.Tweets((Extensions.Twitter.Auth)Common.GetAuthObject(extId, authKeys, null)).UpdateStatus(Common.TryGetString(objParams.FirstOrDefault(k => k.key == "tweetstatus")));
                return System.Net.HttpStatusCode.OK.ToString();
            }

            if (extId.Equals(new Extensions.LinkedIn.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var message = objParams.FirstOrDefault(k => k.key == "linkedinsharestatus");
                if (message == null) throw new Exception("Invalid linkedin share status");

                var accessToken = authKeys.FirstOrDefault(k => k.key == "AccessToken");
                if (accessToken == null) throw new Exception("Invalid access Token");

                new Extensions.LinkedIn.Share(accessToken.value).LinkedInShare(message.value);
                return System.Net.HttpStatusCode.OK.ToString();
            }

            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var key = authKeys.FirstOrDefault(k => k.key == "clientId");
                if (key == null) throw new Exception("Invalid client Id.");

                var secret = authKeys.FirstOrDefault(k => k.key == "clientSecret");
                if (secret == null) throw new Exception("Invalid client secret.");

                var accessToken = authKeys.FirstOrDefault(k => k.key == "accessToken");
                if (accessToken == null) throw new Exception("Invalid access token.");

                var orgId = authKeys.FirstOrDefault(k => k.key == "orgId");
                if (orgId == null) throw new Exception("Invalid organisation.");

                new Extensions.Podio.Tasks(new Extensions.Podio.Auth(key.value, secret.value, accessToken.value, Convert.ToInt32(orgId.value)))
                    .Add(Convert.ToInt32(Common.TryGetInt(objParams.FirstOrDefault(k => k.key == "taskitem"))),
                         Common.TryGetString(objParams.FirstOrDefault(k => k.key == "taskdesc")),
                         Common.TryGetInt(objParams.FirstOrDefault(k => k.key == "taskassignee")),
                         Common.TryGetInt(objParams.FirstOrDefault(k => k.key == "taskduedays")));

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