using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Net.Http;
using Extensions.Asana;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace FlowplaneExtensions.Models.api.Flow
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
            if (extId.Equals(new Extensions.Wrike.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var assignees = objParams.FirstOrDefault(k => k.key == "taskassignee");
                var folders = objParams.FirstOrDefault(k => k.key == "taskfolders");
                var addParams = new NameValueCollection { { Extensions.Wrike.Auth.Parameters.TaskTitle, Common.TryGetString(objParams.FirstOrDefault(k => k.key == "taskdesc")) } };

                if (assignees != null)
                    addParams.Add(Extensions.Wrike.Auth.Parameters.TaskAssignee, assignees.value);
                if (folders != null)
                    addParams.Add(Extensions.Wrike.Auth.Parameters.TaskParentIDs, string.Join(",", JsonConvert.DeserializeObject<List<string>>(folders.value)));

                new Extensions.Wrike.Tasks((Extensions.Wrike.Auth)Common.GetAuthObject(extId, authKeys, objParams))
                    .Create(addParams);
                return System.Net.HttpStatusCode.OK.ToString();
            }
            if (extId.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = authKeys.FirstOrDefault(k => k.key == "API_Key");
                if (apiKey == null) throw new Exception("Invalid API_Key");

                var username = authKeys.FirstOrDefault(k => k.key == "username");
                if (username == null) throw new Exception("Paymo user name is mandatory.");

                var password = authKeys.FirstOrDefault(k => k.key == "password");
                if (password == null) throw new Exception("Paymo password is mandatory.");

                var projectId = objParams.FirstOrDefault(k => k.key == "taskproject");
                if (projectId == null) throw new Exception("Paymo project id is mandatory.");

                var tasklistId = objParams.FirstOrDefault(k => k.key == "tasktasklist");
                if (tasklistId == null) throw new Exception("Paymo task list is mandatory.");

                var name = objParams.FirstOrDefault(k => k.key == "taskdesc");
                if (name == null) throw new Exception("Paymo task name is mandatory.");

                var assigneeId = objParams.FirstOrDefault(k => k.key == "taskassignee");
                if (assigneeId == null) throw new Exception("Paymo task assignee is mandatory.");

                var dueDate = objParams.FirstOrDefault(k => k.key == "taskduedays");
                int dd = 0;
                if (dueDate != null)
                    Int32.TryParse(dueDate.value, out dd);

                new Extensions.Paymo.Tasks(new Extensions.Paymo.Auth(apiKey.value,username.value,password.value)).Add(projectId.value, tasklistId.value, name.value, assigneeId.value, dd);
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