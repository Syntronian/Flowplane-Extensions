using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Cors;
using FlowplaneExtensions.Models.api;

namespace FlowplaneExtensions.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Api_FlowController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public string ActivateObject(FormDataCollection formData)
        {
            return new Models.api.flow.Exec().ActivateObject(formData);
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage CompleteActivity(FormDataCollection formData)
        {
            return new Models.api.flow.Exec().CompleteActivity(formData);
        }
    }
}