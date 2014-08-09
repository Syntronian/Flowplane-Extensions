using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ExtensionsCore;
using System.Net.Http.Formatting;

namespace FlowplaneExtensions.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Api_ProcessController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public IAssignees GetAssignees(FormDataCollection formData)
        {
            int orgId;
            Int32.TryParse(Common.GetValue(formData, "organisationId"), out orgId);
            return new Models.api.Process.Details().GetAssignees(Common.GetValue(formData, "extId")
                                                                , Common.GetValue(formData, "apiKey")
                                                                , Common.GetValue(formData, "username")
                                                                , Common.GetValue(formData, "password")
                                                                , Common.GetValue(formData, "clientId")
                                                                , Common.GetValue(formData, "clientSecret")
                                                                , Common.GetValue(formData, "accessToken")
                                                                , orgId);
        }

        [HttpPost]
        [AllowAnonymous]
        public IWorkspaces GetWorkSpaces(FormDataCollection formData)
        {
            return new Models.api.Process.Details().GetWorkSpaces(Common.GetValue(formData, "extId")
                                                                , Common.GetValue(formData, "apiKey"));
        }

        [HttpPost]
        [AllowAnonymous]
        public IProjects GetProjects(FormDataCollection formData)
        {
            bool archived;
            return new Models.api.Process.Details().GetProjects(Common.GetValue(formData, "extId")
                                                    , Common.GetValue(formData, "apiKey")
                                                    , Common.GetValue(formData, "workspaceId")
                                                    , Boolean.TryParse(Common.GetValue(formData, "archived"), out archived) ? (bool?)archived : null
                                                    , Common.GetValue(formData, "username")
                                                    , Common.GetValue(formData, "password")
                                                    , Common.GetValue(formData, "authToken"));
        }

        [HttpPost]
        [AllowAnonymous]
        public IOrganisations GetOrganisations(FormDataCollection formData)
        {
            int orgId;
            Int32.TryParse(Common.GetValue(formData, "organizationId"),out orgId);
            return new Models.api.Process.Details().GetOrganizations(Common.GetValue(formData, "extId")
                                                    , Common.GetValue(formData, "clientId")
                                                    , Common.GetValue(formData, "clientSecret")
                                                    , Common.GetValue(formData, "accessToken")
                                                    , orgId);

        }

    }
}
