﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExtensionsCore;

namespace FlowplaneExtensions.Models.api.Register
{
    public class GetAll
    {
        public class Extension
        {
            public IIdentity identity { get; set; }
            public string url_path_auth { get; set; }
            public string url_path_header { get; set; }
            public string url_path_body { get; set; }
            public string img_src { get; set; }
            public List<AuthKey> auth_keys { get; set; }
        }

        public List<Extension> extensions { get; set; }

        public GetAll()
        {
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var urlPrefix = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

            this.extensions = new List<Extension>
            {
                new Extension()
                {
                    identity = new Extensions.Asana.Identity(),
                    url_path_auth = urlPrefix + url.Action("Auth", "Asana"),
                    url_path_header = urlPrefix + url.Action("Header", "Asana"),
                    url_path_body = urlPrefix + url.Action("Body", "Asana"),
                    img_src = urlPrefix + url.Content("~//Content/img/ext-asana.png"),
                    auth_keys = new List<AuthKey>()
                    {
                        new AuthKey() {
                            key = "API_Key",
                            controlId = "txtAsanaAPIKey",
                            controlDescription = "API key",
                            required = true,
                            requiredMessage = "API key is required."
                        }
                    }
                },

                new Extension()
                {
                    identity = new Extensions.Paymo.Identity(),
                    url_path_auth = urlPrefix + url.Action("Auth", "Paymo"),
                    url_path_header = urlPrefix + url.Action("Header", "Paymo"),
                    url_path_body = urlPrefix + url.Action("Body", "Paymo"),
                    img_src = urlPrefix + url.Content("~//Content/img/ext-paymo.png"),
                    auth_keys = new List<AuthKey>()
                    {
                        new AuthKey() {
                            key = "API_Key",
                            controlId = "txtPaymoAPIKey",
                            controlDescription = "API key",
                            required = true,
                            requiredMessage = "API key is required."
                        },
                        new AuthKey() {
                            key = "username",
                            controlId = "txtPaymoUsername",
                            controlDescription = "Username",
                            required = true,
                            requiredMessage = "Username is required."
                        },
                        new AuthKey() {
                            key = "password",
                            controlId = "txtPaymoPassword",
                            controlDescription = "Password",
                            required = true,
                            requiredMessage = "Password is required."
                        }
                    }
                }
            };
        }
    }
}