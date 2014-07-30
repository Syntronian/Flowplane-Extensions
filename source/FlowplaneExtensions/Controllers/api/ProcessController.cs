using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FlowplaneExtensions.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProcessController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public dynamic GetAssignees(string extid)
        {
            var det = new Models.api.Process.Details();
            return det.GetAssignees(extid);

        }
    }
}
