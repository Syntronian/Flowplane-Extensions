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

            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var clientId = authKeys.FirstOrDefault(k => k.key == "ClientId");
                if (clientId == null) throw new Exception("Invalid client Id");

                var clientSecret = authKeys.FirstOrDefault(k => k.key == "ClientSecret");
                if (clientSecret == null) throw new Exception("Invalid client secret");

                var accessToken = authKeys.FirstOrDefault(k => k.key == "AccessToken");
                if (accessToken == null) throw new Exception("Invalid access token");

                return new Extensions.Podio.Users(
                    new Extensions.Podio.Auth(
                        clientId.value, 
                        clientSecret.value, 
                        accessToken.value, 
                        Common.TryGetInt(authKeys.FirstOrDefault(k => k.key == "orgId")))).List();
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

            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
                return new Extensions.Podio.Workspaces(Common.GetAuthObject(authKeys)).List();
            
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

        public IApps GetApps(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);

            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var spaceId = objParams.FirstOrDefault(k => k.key == "spaceId");
                int id;
                if (spaceId == null) throw new Exception("Invalid spaceId");
                else Int32.TryParse(spaceId.value, out id);

                return new Extensions.Podio.Apps(Common.GetAuthObject(authKeys)).List(id);
            }

            throw new Exception("Invalid extension.");
        }

        public IItems GetItems(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);

            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var appId = objParams.FirstOrDefault(k => k.key == "appId");
                int id;
                if (appId == null) throw new Exception("Invalid appId");
                else Int32.TryParse(appId.value, out id);

                return new Extensions.Podio.Items(Common.GetAuthObject(authKeys)).List(id);
            }

            throw new Exception("Invalid extension.");
        }

        public IOrganisations GetOrganizations(FormDataCollection formData)
        {
            var extId = Common.GetValue(formData, "extId");
            var authKeys = Common.TryGetParams(formData["authKeys"]);
            var objParams = Common.TryGetParams(formData["objParams"]);
            
            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
                return new Extensions.Podio.Orgs(Common.GetAuthObject(authKeys)).List();
        
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