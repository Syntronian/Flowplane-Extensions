using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using Extensions.Asana;
using ExtensionsCore;

namespace FlowplaneExtensions.Models.api.Process
{
    public class Details
    {
        public IAssignees GetAssignees(string extId, FormDataCollection data)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = data["API_Key"];
                
                return new Extensions.Asana.Users(new Auth(apiKey)).List();
            }

            if (extId.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = data["API_Key"];
                var username = data["username"];
                var password = data["password"];

                if (username == null) throw new Exception("Paymo user name is mandatory.");
                if (password == null) throw new Exception("Paymo password is mandatory.");
                return new Extensions.Paymo.Users(new Extensions.Paymo.Auth(apiKey, username, password)).List();
            }

            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var clientId = data["clientId"];
                var clientSecret = data["clientSecret"];
                var accessToken = data["accessToken"];
                int organisationId;
                Int32.TryParse(data["organisationId"], out organisationId);

                if (clientId == null) throw new Exception("Podio client id is mandatory.");
                if (clientSecret == null) throw new Exception("Podio client secret is mandatory.");
                if (accessToken == null) throw new Exception("Podio auth token is mandatory.");
                return new Extensions.Podio.Users(new Extensions.Podio.Auth(clientId, clientSecret, accessToken, organisationId)).List();
            }

            throw new Exception("Invalid extension.");
        }



        public IWorkspaces GetWorkSpaces(string extId, FormDataCollection data)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = data["API_Key"];
                
                return new Workspaces(new Auth(apiKey)).List();
            }

            throw new Exception("Invalid extension.");
        }



        public IProjects GetProjects(string extId, FormDataCollection data)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = data["API_Key"];
                var workspaceId = data["workspaceId"];
                bool archived;

                return new Projects(new Auth(apiKey)).List(
                    workspaceId,
                    Boolean.TryParse(data["archived"], out archived) ? (bool?)archived : null);
            }

            if (extId.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var apiKey = data["API_Key"];
                var username = data["username"];
                var password = data["password"];
                var authToken = data["authToken"];

                if (username == null) throw new Exception("Paymo user name is mandatory.");
                if (password == null) throw new Exception("Paymo password is mandatory.");
                return new Extensions.Paymo.Projects(new Extensions.Paymo.Auth(apiKey, username, password, authToken)).List();
            }

            throw new Exception("Invalid extension.");
        }



        public IOrganisations GetOrganizations(string extId, FormDataCollection data)
        {
            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var clientId = data["clientId"];
                var clientSecret = data["clientSecret"];
                var accessToken = data["accessToken"];
                int organisationId;
                Int32.TryParse(data["organisationId"], out organisationId);

                return new Extensions.Podio.Orgs(new Extensions.Podio.Auth(clientId,
                                                     clientSecret,
                                                     accessToken,
                                                     organisationId)).List();
            }

            throw new Exception("Invalid extension.");
        }
    }
}