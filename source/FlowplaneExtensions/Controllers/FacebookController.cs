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
    public class FacebookController : Controller
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
        public ActionResult oauth(string txtFacebookAppId, string txtFacebookAppSecret)
        {
            TempData["txtFacebookAppId"] = txtFacebookAppId;
            TempData["txtFacebookAppSecret"] = txtFacebookAppSecret;

            if (string.IsNullOrEmpty(txtFacebookAppId) && string.IsNullOrEmpty(txtFacebookAppSecret))
            {
                // use flowplane
                var httpClient = new HttpClient();
                var rs = httpClient.GetAsync(
                    string.Format(Common.FlowplaneDotCom + "/api/oauth/getloginurl/{0}?returnurl={1}",
                        new Extensions.Facebook.Identity().Code,
                        Request.Url.AbsoluteUri)).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

                var url = JsonConvert.DeserializeObject<string>(rs.Content.ReadAsStringAsync().Result);

                return Redirect(HttpUtility.UrlDecode(url));
            }
            else
            {
                var auth = new Extensions.Facebook.Auth();
                return Redirect(HttpUtility.UrlDecode(auth.GetLoginUrl(txtFacebookAppId, txtFacebookAppSecret, Request.Url.AbsoluteUri)));
            }
        }

        public ActionResult oauth()
        {
            if (Request["code"] == null)
                throw new Exception("Invalid auth code.");

            var txtFacebookAppId = TempData["txtFacebookAppId"].ToString();
            var txtFacebookAppSecret = TempData["txtFacebookAppSecret"].ToString();

            if (string.IsNullOrEmpty(txtFacebookAppId) && string.IsNullOrEmpty(txtFacebookAppSecret))
            {
                // use flowplane
                var httpClient = new HttpClient();
                var rs = httpClient.GetAsync(
                    string.Format(Common.FlowplaneDotCom + "/api/oauth/getaccesstoken/{0}?code={1}&returnurl={2}",
                                  new Extensions.Facebook.Identity().Code, 
                                  Request["code"], 
                                  Request.Url.AbsoluteUri)).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

                ViewBag.AccessToken = JsonConvert.DeserializeObject<string>(rs.Content.ReadAsStringAsync().Result);

                return View("OAuthComplete");
            }
            else
            {
                var auth = new Extensions.Facebook.Auth();
                ViewBag.AccessToken = auth.GetAccessToken(txtFacebookAppId, txtFacebookAppSecret, Request["code"], Request.Url.AbsoluteUri);

                return View("OAuthComplete");
            }
        }
    }
}