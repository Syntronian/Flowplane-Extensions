using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FlowplaneExtensions.Controllers.api
{
    public class Api_RegisterController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public Models.api.Register.GetAll GetAll()
        {
            return new Models.api.Register.GetAll();
        }

        [HttpGet]
        [AllowAnonymous]
        public string GetObjectTypeNameActivity()
        {
            return ExtensionsCore.Definitions.ProcessObjectTypes.Activity.ToString();
        }

        [HttpGet]
        [AllowAnonymous]
        public string GetObjectTypeNameEvent()
        {
            return ExtensionsCore.Definitions.ProcessObjectTypes.Event.ToString();
        }
    }
}