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
    public class Api_Register_GetAllController : ApiController
    {
        [AllowAnonymous]
        public Models.api.Register.GetAll Get(int id)
        {
            return new Models.api.Register.GetAll();
        }
    }
}