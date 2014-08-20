using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;

using ExtensionsCore;
using Extensions.Asana;

namespace FlowplaneExtensions.Models.api.Process
{
    public class Details
    {
        public IAssignees GetAssignees(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);

            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = authKeys.FirstOrDefault(k => k.key == "API_Key");
                if (apiKey == null) throw new Exception("Invalid API_Key");
                
                return new Extensions.Asana.Users(new Auth(apiKey.value)).List();
            }

            if (extId.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = authKeys.FirstOrDefault(k => k.key == "API_Key");
                if (apiKey == null) throw new Exception("Invalid API_Key");

                var username = authKeys.FirstOrDefault(k => k.key == "username");
                if (username == null) throw new Exception("Paymo user name is mandatory.");

                var password = authKeys.FirstOrDefault(k => k.key == "password");
                if (password == null) throw new Exception("Paymo password is mandatory.");

                return new Extensions.Paymo.Users(new Extensions.Paymo.Auth(apiKey.value, username.value, password.value)).List();
            }

            if (extId.Equals(new Extensions.Wrike.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
                return new Extensions.Wrike.Users((Extensions.Wrike.Auth)Common.GetAuthObject(extId, authKeys, null)).Get();
            
            throw new Exception("Invalid extension.");
        }



        public IWorkspaces GetWorkSpaces(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);
            
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = authKeys.FirstOrDefault(k => k.key == "API_Key");
                if (apiKey == null) throw new Exception("Invalid API_Key");

                return new Extensions.Asana.Workspaces(new Extensions.Asana.Auth(apiKey.value)).List();
            }

            throw new Exception("Invalid extension.");
        }



        public IProjects GetProjects(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);
            
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = authKeys.FirstOrDefault(k => k.key == "API_Key");
                if (apiKey == null) throw new Exception("Invalid API_Key");
                
                return new Projects(new Auth(apiKey.value)).List(
                    Common.TryGetString(objParams.FirstOrDefault(k => k.key == "workspaceId")) ?? "",
                    Common.TryGetBool(objParams.FirstOrDefault(k => k.key == "archived")));
            }

            if (extId.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = authKeys.FirstOrDefault(k => k.key == "API_Key");
                if (apiKey == null) throw new Exception("Invalid API_Key");

                var username = authKeys.FirstOrDefault(k => k.key == "username");
                if (username == null) throw new Exception("Paymo user name is mandatory.");

                var password = authKeys.FirstOrDefault(k => k.key == "password");
                if (password == null) throw new Exception("Paymo password is mandatory.");

                var authToken = authKeys.FirstOrDefault(k => k.key == "authToken");

                return new Extensions.Paymo.Projects(
                    new Extensions.Paymo.Auth(
                        apiKey.value, username.value, password.value, Common.TryGetString(authToken))).List();
            }

            throw new Exception("Invalid extension.");
        }

        public ITasks GetTasks(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);

            if (extId.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = authKeys.FirstOrDefault(k => k.key == "API_Key");
                if (apiKey == null) throw new Exception("Invalid API_Key");

                var username = authKeys.FirstOrDefault(k => k.key == "username");
                if (username == null) throw new Exception("Paymo user name is mandatory.");

                var password = authKeys.FirstOrDefault(k => k.key == "password");
                if (password == null) throw new Exception("Paymo password is mandatory.");

                var authToken = authKeys.FirstOrDefault(k => k.key == "authToken");

                var projectId = objParams.FirstOrDefault(k => k.key == "project_id");

                return new Extensions.Paymo.TaskLists(
                    new Extensions.Paymo.Auth(
                        apiKey.value, 
                        username.value, 
                        password.value, 
                        Common.TryGetString(authToken))).List(Common.TryGetString(projectId));
            }

            throw new Exception("Invalid extension.");
        }

        
    }
}