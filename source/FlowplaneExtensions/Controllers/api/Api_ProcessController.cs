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
            var extId = formData.FirstOrDefault(a => a.Key == "extId").Value;
            var apiKey = formData.FirstOrDefault(a => a.Key == "apiKey").Value;

            var det = new Models.api.Process.Details();
            return det.GetAssignees(extId, apiKey);
        }
    }
}
