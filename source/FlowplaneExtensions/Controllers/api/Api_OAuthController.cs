using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
namespace FlowplaneExtensions.Controllers.api
{
    public class Api_OAuthController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public string GetLoginUrl(FormDataCollection formData)
        {
            return new Models.api.OAuth.Auth().GetLoginUrl(formData);
        }

        [HttpPost]
        [AllowAnonymous]
        public string GetAccessToken(FormDataCollection formData)
        {
            return new Models.api.OAuth.Auth().GetAccessToken(formData);
        }

    }
}