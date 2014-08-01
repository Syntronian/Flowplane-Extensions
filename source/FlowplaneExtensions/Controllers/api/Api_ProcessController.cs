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
            return new Models.api.Process.Details().GetAssignees(GetValue(formData, "extId")
                                                                , GetValue(formData, "apiKey")
                                                                , GetValue(formData, "userName")
                                                                , GetValue(formData, "password"));
        }

        [HttpPost]
        [AllowAnonymous]
        public Extensions.Asana.Responses.Workspaces.List GetWorkSpaces(FormDataCollection formData)
        {
            return new Models.api.Process.Details().GetWorkSpaces(GetValue(formData, "extId")
                                                                , GetValue(formData, "apiKey"));
        }

        [HttpPost]
        [AllowAnonymous]
        public IProjects GetProjects(FormDataCollection formData)
        {
            bool archived;
            return new Models.api.Process.Details().GetProjects(GetValue(formData, "extId")
                                                    , GetValue(formData, "apiKey")
                                                    , GetValue(formData, "workspaceId")
                                                    , Boolean.TryParse(GetValue(formData, "archived"), out archived) ? (bool?)archived : null
                                                    , GetValue(formData, "userName")
                                                    , GetValue(formData, "password")
                                                    , GetValue(formData, "authToken"));
        }

        private string GetValue(FormDataCollection data, string key)
        {
            return data.FirstOrDefault(a => a.Key == key).Value;
        }

    }
}
