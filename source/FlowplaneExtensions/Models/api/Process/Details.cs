using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Extensions.Asana;
using ExtensionsCore;

namespace FlowplaneExtensions.Models.api.Process
{
    public class Details
    {
        public IAssignees GetAssignees(string extId,
                                       string apiKey = null,
                                       string username = null,
                                       string password = null,
                                       string clientId = null,
                                       string clientSecret = null,
                                       string accessToken = null,
                                       int? organisationId = null)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Extensions.Asana.Users(new Auth(apiKey)).List();
            }

            if (extId.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                if (username == null) throw new Exception("Paymo user name is mandatory.");
                if (password == null) throw new Exception("Paymo password is mandatory.");
                return new Extensions.Paymo.Users(new Extensions.Paymo.Auth(apiKey, username, password)).List();
            }

            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                if (clientId == null) throw new Exception("Podio client id is mandatory.");
                if (clientSecret == null) throw new Exception("Podio client secret is mandatory.");
                if (accessToken == null) throw new Exception("Podio auth token is mandatory.");
                return new Extensions.Podio.Users(new Extensions.Podio.Auth(clientId, clientSecret, accessToken, organisationId)).List();
            }

            throw new Exception("Invalid extension.");
        }



        public IWorkspaces GetWorkSpaces(string extId,
                                         string apiKey)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Workspaces(new Auth(apiKey)).List();
            }

            throw new Exception("Invalid extension.");
        }



        public IProjects GetProjects(string extId,
                                     string apiKey,
                                     string workspaceId = "",
                                     bool? archived = null,
                                     string username = null,
                                     string password = null,
                                     string authToken = null)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Projects(new Auth(apiKey)).List(workspaceId, archived);
            }

            if (extId.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                if (username == null) throw new Exception("Paymo user name is mandatory.");
                if (password == null) throw new Exception("Paymo password is mandatory.");
                return new Extensions.Paymo.Projects(new Extensions.Paymo.Auth(apiKey, username, password, authToken)).List();
            }

            throw new Exception("Invalid extension.");
        }



        public IOrganisations GetOrganizations(string extId,
                                               string clientId,
                                               string clientSecret,
                                               string accessToken,
                                               int? organisationId = null)
        {
            if (extId.Equals(new Extensions.Podio.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Extensions.Podio.Orgs(new Extensions.Podio.Auth(clientId,
                                                     clientSecret,
                                                     accessToken,
                                                     organisationId)).List();
            }

            throw new Exception("Invalid extension.");
        }
    }
}