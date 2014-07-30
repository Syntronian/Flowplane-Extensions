using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extensions.Asana;
using ExtensionsCore;

namespace FlowplaneExtensions.Models.api.Register
{
    public class GetAll
    {
        public enum CodeList
        {
            Asana = 1
        }

        public class Extension
        {
            public IIdentity identity { get; set; }
            public string url_path_auth { get; set; }
            public string url_path_header { get; set; }
            public string url_path_body { get; set; }
        }

        public List<Extension> extensions { get; set; }

        public GetAll()
        {
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            this.extensions = new List<Extension>
            {
                new Extension()
                {
                    identity = new Extensions.Asana.Identity(),
                    url_path_auth = url.Action("Auth", "Asana"),
                    url_path_header = url.Action("Header", "Asana"),
                    url_path_body = url.Action("Body", "Asana")
                }
            };
        }
    }
}