using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Extensions.Asana.Responses.Projects;
using FlowplaneExtensions.Models.api;
using Newtonsoft.Json;

namespace FlowplaneExtensions.Controllers
{
    public class LinkedInController : Controller
    {
        public ActionResult Auth()
        {
            return View();
        }
        public ActionResult Header()
        {
            return View();
        }
        public ActionResult Body()
        {
            return View();
        }

        [HttpPost]
        public ActionResult oauth(string txtLinkedInApiKey, string txtLinkedInApiSecret)
        {
            TempData["txtLinkedInApiKey"] = txtLinkedInApiKey;
            TempData["txtLinkedInApiSecret"] = txtLinkedInApiSecret;

            if (string.IsNullOrEmpty(txtLinkedInApiKey) && string.IsNullOrEmpty(txtLinkedInApiSecret))
            {
                // use flowplane
                var httpClient = new HttpClient();
                var rs = httpClient.GetAsync(
                    string.Format(Common.FlowplaneDotCom + "/api/oauth/getloginurl/{0}?returnurl={1}",
                                  new Extensions.LinkedIn.Identity().Code, Request.Url.AbsoluteUri)).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

                var url = JsonConvert.DeserializeObject<string>(rs.Content.ReadAsStringAsync().Result);

                return Redirect(HttpUtility.UrlDecode(url));
            }
            else
            {
                var auth = new Extensions.LinkedIn.Tokens(new Extensions.LinkedIn.Auth(txtLinkedInApiKey, txtLinkedInApiSecret, "", Request.Url.AbsoluteUri));
                return Redirect(HttpUtility.UrlDecode(auth.GetRequestURL()));
            }
        }

        public ActionResult oauth()
        {
            if (Request["code"] == null)
                throw new Exception("Invalid auth codes.");

            var txtLinkedInApiKey = TempData["txtLinkedInApiKey"].ToString();
            var txtLinkedInApiSecret = TempData["txtLinkedInApiSecret"].ToString();
            var redirectUri = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            if (string.IsNullOrEmpty(txtLinkedInApiKey) && string.IsNullOrEmpty(txtLinkedInApiSecret))
            {
                // use flowplane
                var httpClient = new HttpClient();
                var rs = httpClient.GetAsync(
                    string.Format(Common.FlowplaneDotCom + "/api/oauth/getaccesstokens/{0}?returnurl={2}&linkedinCode={1}",
                                  new Extensions.LinkedIn.Identity().Code,
                                  Request["code"],
                                  redirectUri)).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

                var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(rs.Content.ReadAsStringAsync().Result);

                ViewBag.LinkedInAccessToken = token["AccessToken"];
            }
            else
            {
                var auth = new Extensions.LinkedIn.Tokens(new Extensions.LinkedIn.Auth(txtLinkedInApiKey, txtLinkedInApiSecret, "", redirectUri));
                var token = auth.GetAccessToken(Request["code"]);

                ViewBag.LinkedInAccessToken = token;
            }

            return View("OAuthComplete");
        }
    }
}