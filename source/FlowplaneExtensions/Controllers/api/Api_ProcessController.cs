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
            return new Models.api.Process.Details().GetAssignees(Common.GetValue(formData, "extId"), formData);
        }

        [HttpPost]
        [AllowAnonymous]
        public IWorkspaces GetWorkSpaces(FormDataCollection formData)
        {
            return new Models.api.Process.Details().GetWorkSpaces(Common.GetValue(formData, "extId"), formData);
        }

        [HttpPost]
        [AllowAnonymous]
        public IProjects GetProjects(FormDataCollection formData)
        {
            return new Models.api.Process.Details().GetProjects(Common.GetValue(formData, "extId"), formData);
        }

        [HttpPost]
        [AllowAnonymous]
        public IOrganisations GetOrganisations(FormDataCollection formData)
        {
            return new Models.api.Process.Details().GetOrganizations(Common.GetValue(formData, "extId"), formData);
        }
    }
}
