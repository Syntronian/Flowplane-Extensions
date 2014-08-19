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
    public class TwitterController : Controller
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
        public ActionResult oauth(string txtTwitterConsumerKey, string txtTwitterConsumerSecret)
        {
            TempData["txtTwitterConsumerKey"] = txtTwitterConsumerKey;
            TempData["txtTwitterConsumerSecret"] = txtTwitterConsumerSecret;

            if (string.IsNullOrEmpty(txtTwitterConsumerKey) && string.IsNullOrEmpty(txtTwitterConsumerSecret))
            {
                // use flowplane
                var httpClient = new HttpClient();
                var rs = httpClient.GetAsync(
                    string.Format(Common.FlowplaneDotCom + "/api/oauth/getloginurl/{0}?returnurl={1}",
                                  new Extensions.Twitter.Identity().Code, Request.Url.AbsoluteUri)).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

                var url = JsonConvert.DeserializeObject<string>(rs.Content.ReadAsStringAsync().Result);

                return Redirect(HttpUtility.UrlDecode(url));
            }
            else
            {
                var auth = new Extensions.Twitter.Auth(txtTwitterConsumerKey, txtTwitterConsumerSecret, "", "");
                return Redirect(HttpUtility.UrlDecode(auth.GetLoginUrl(Request.Url.AbsoluteUri)));
            }
        }

        public ActionResult oauth()
        {
            if (Request["oauth_token"] == null || Request["oauth_verifier"] == null)
                throw new Exception("Invalid auth codes.");

            var txtTwitterConsumerKey = TempData["txtTwitterConsumerKey"].ToString();
            var txtTwitterConsumerSecret = TempData["txtTwitterConsumerSecret"].ToString();

            if (string.IsNullOrEmpty(txtTwitterConsumerKey) && string.IsNullOrEmpty(txtTwitterConsumerSecret))
            {
                // use flowplane
                var httpClient = new HttpClient();
                var rs = httpClient.GetAsync(
                    string.Format(Common.FlowplaneDotCom + "/api/oauth/getaccesstokens/{0}?returnurl=&facebookCode=&twitterOauth_token={1}&twitterOauth_verifier={2}",
                                  new Extensions.Twitter.Identity().Code,
                                  Request["oauth_token"],
                                  Request["oauth_verifier"])).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

                var tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(rs.Content.ReadAsStringAsync().Result);

                ViewBag.AccessToken = tokens["AccessToken"];
                ViewBag.AccessTokenSecret = tokens["AccessTokenSecret"];
            }
            else
            {
                var auth = new Extensions.Twitter.Auth(txtTwitterConsumerKey, txtTwitterConsumerSecret, "", "");
                var tokens = auth.GetAccessTokens(Request["oauth_token"], Request["oauth_verifier"]);

                ViewBag.AccessToken = tokens["AccessToken"];
                ViewBag.AccessTokenSecret = tokens["AccessTokenSecret"];
            }

            return View("OAuthComplete");
        }
    }
}