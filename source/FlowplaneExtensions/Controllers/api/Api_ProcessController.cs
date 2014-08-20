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
            return new Models.api.Process.Details().GetAssignees(formData);
        }

        [HttpPost]
        [AllowAnonymous]
        public IWorkspaces GetWorkSpaces(FormDataCollection formData)
        {
            return new Models.api.Process.Details().GetWorkSpaces(formData);
        }

        [HttpPost]
        [AllowAnonymous]
        public IProjects GetProjects(FormDataCollection formData)
        {
            return new Models.api.Process.Details().GetProjects(formData);
        }

        [HttpPost]
        [AllowAnonymous]
        public ITasks GetTasks(FormDataCollection formData)
        {
            return new Models.api.Process.Details().GetTasks(formData);
        }
    }
}
