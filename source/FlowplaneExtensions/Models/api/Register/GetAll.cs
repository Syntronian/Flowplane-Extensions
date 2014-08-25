using System;
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
                    identity = new Extensions.Facebook.Identity(),
                    url_path_auth = urlPrefix + url.Action("Auth", "Facebook"),
                    url_path_header = urlPrefix + url.Action("Header", "Facebook"),
                    url_path_body = urlPrefix + url.Action("Body", "Facebook"),
                    img_src = urlPrefix + url.Content("~//Content/img/ext-facebook.png"),
                    auth_keys = new List<AuthKey>()
                    {
                        new AuthKey() {
                            key = "AppId",
                            controlId = "txtFacebookAppId",
                            controlDescription = "App Id",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "AppSecret",
                            controlId = "txtFacebookAppSecret",
                            controlDescription = "App Secret",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "AccessToken",
                            controlId = "txtFacebookAccessToken",
                            controlDescription = "Access token",
                            required = true,
                            requiredMessage = "Access token is required, connect to Facebook first."
                        }
                    }
                },

                new Extension()
                {
                    identity = new Extensions.Twitter.Identity(),
                    url_path_auth = urlPrefix + url.Action("Auth", "Twitter"),
                    url_path_header = urlPrefix + url.Action("Header", "Twitter"),
                    url_path_body = urlPrefix + url.Action("Body", "Twitter"),
                    img_src = urlPrefix + url.Content("~//Content/img/ext-twitter.png"),
                    auth_keys = new List<AuthKey>()
                    {
                        new AuthKey() {
                            key = "ConsumerKey",
                            controlId = "txtTwitterConsumerKey",
                            controlDescription = "Consumer key",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "ConsumerSecret",
                            controlId = "txtTwitterConsumerSecret",
                            controlDescription = "Consumer secret",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "AccessToken",
                            controlId = "txtTwitterAccessToken",
                            controlDescription = "Access token",
                            required = true,
                            requiredMessage = "Access token is required, connect to Twitter first."
                        },
                        new AuthKey() {
                            key = "AccessTokenSecret",
                            controlId = "txtTwitterAccessTokenSecret",
                            controlDescription = "Secret access token",
                            required = true,
                            requiredMessage = "Secret access token is required, connect to Twitter first."
                        }
                    }
                },

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
                },

                new Extension()
                {
                    identity = new Extensions.Podio.Identity(),
                    url_path_auth = urlPrefix + url.Action("Auth", "Podio"),
                    url_path_header = urlPrefix + url.Action("Header", "Podio"),
                    url_path_body = urlPrefix + url.Action("Body", "Podio"),
                    img_src = urlPrefix + url.Content("~//Content/img/ext-podio.png"),
                    auth_keys = new List<AuthKey>()
                    {
                        new AuthKey() {
                            key = "clientId",
                            controlId = "txtPodioAppId",
                            controlDescription = "Client Id",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "clientSecret",
                            controlId = "txtPodioAppSecret",
                            controlDescription = "Client Secret",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "accessToken",
                            controlId = "txtPodioAccessToken",
                            controlDescription = "Access token",
                            required = true,
                            requiredMessage = "Connect to podio first."
                        },
                        new AuthKey() {
                            key = "orgId",
                            controlId = "cboPodioOrg",
                            controlDescription = "Organisation",
                            required = true,
                            requiredMessage = "organisation is required."
                        }
                    }
                },

                new Extension()
                {
                    identity = new Extensions.LinkedIn.Identity(),
                    url_path_auth = urlPrefix + url.Action("Auth", "LinkedIn"),
                    url_path_header = urlPrefix + url.Action("Header", "LinkedIn"),
                    url_path_body = urlPrefix + url.Action("Body", "LinkedIn"),
                    img_src = urlPrefix + url.Content("~//Content/img/ext-linkedin.png"),
                    auth_keys = new List<AuthKey>()
                    {
                        new AuthKey() {
                            key = "ConsumerKey",
                            controlId = "txtLinkedInApiKey",
                            controlDescription = "Api key",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "ConsumerSecret",
                            controlId = "txtLinkedInApiSecret",
                            controlDescription = "Api secret",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "AccessToken",
                            controlId = "txtLinkedInAccessToken",
                            controlDescription = "Access token",
                            required = true,
                            requiredMessage = "Access token is required, connect to LinkedIn first."
                        }
                    }
                },
                new Extension()
                {
                    identity = new Extensions.Wrike.Identity(),
                    url_path_auth = urlPrefix + url.Action("Auth", "Wrike"),
                    url_path_header = urlPrefix + url.Action("Header", "Wrike"),
                    url_path_body = urlPrefix + url.Action("Body", "Wrike"),
                    img_src = urlPrefix + url.Content("~//Content/img/ext-wrike.png"),
                    auth_keys = new List<AuthKey>()
                    {
                        new AuthKey() {
                            key = "ConsumerKey",
                            controlId = "txtWrikeConsumerKey",
                            controlDescription = "Consumer key",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "ConsumerSecret",
                            controlId = "txtWrikeConsumerSecret",
                            controlDescription = "Consumer secret",
                            required = false,
                            requiredMessage = ""
                        },
                        new AuthKey() {
                            key = "AccessToken",
                            controlId = "txtWrikeAccessToken",
                            controlDescription = "Access token",
                            required = true,
                            requiredMessage = "Access token is required, connect to Wrike first."
                        },
                        new AuthKey() {
                            key = "AccessTokenSecret",
                            controlId = "txtWrikeAccessTokenSecret",
                            controlDescription = "Secret access token",
                            required = true,
                            requiredMessage = "Secret access token is required, connect to Wrike first."
                        }
                    }
                }
            };
        }
    }
}