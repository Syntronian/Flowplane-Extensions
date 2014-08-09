using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
namespace FlowplaneExtensions.Controllers.api
{
    public class Api_FlowController : ApiController
    {
        // POST api/<controller>
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Exec(FormDataCollection formData)
        {
            return new Models.api.flow.Exec().Run(Common.GetValue(formData, "extId"),
                                                  Common.GetValue(formData, "message"),
                                                  Common.GetValue(formData, "linkUrl"),
                                                  Common.GetValue(formData, "pictureUrl"),
                                                  Common.GetValue(formData, "name"),
                                                  Common.GetValue(formData, "caption"),
                                                  Common.GetValue(formData, "description"),
                                                  Common.GetValue(formData, "actionName"),
                                                  Common.GetValue(formData, "actionLinkUrl"),
                                                  Common.GetValue(formData, "appId"),
                                                  Common.GetValue(formData, "appToken"),
                                                  Common.GetValue(formData, "consumerKey"),
                                                  Common.GetValue(formData, "consumerSecret"),
                                                  Common.GetValue(formData, "accessToken"),
                                                  Common.GetValue(formData, "accessTokenSecret"));
        }

    }
}