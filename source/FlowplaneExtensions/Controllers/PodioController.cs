using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

using FlowplaneExtensions.Models.api;

namespace FlowplaneExtensions.Controllers
{
    public class PodioController : Controller
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
        public ActionResult oauth(string txtPodioAppId, string txtPodioAppSecret)
        {
            TempData["txtPodioAppId"] = txtPodioAppId;
            TempData["txtPodioAppSecret"] = txtPodioAppSecret;

            if (string.IsNullOrEmpty(txtPodioAppId) && string.IsNullOrEmpty(txtPodioAppSecret))
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

            throw new Exception("Unsupported extension.");
        }


        public ActionResult oAuth()
        {
            if (Request["code"] == null)
                throw new Exception("Invalid auth code.");

            var txtPodioAppId = TempData["txtPodioAppId"].ToString();
            var txtPodioAppSecret = TempData["txtPodioAppSecret"].ToString();

            if (string.IsNullOrEmpty(txtPodioAppId) && string.IsNullOrEmpty(txtPodioAppSecret))
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

            throw new Exception("Unsupported extension.");
        }
    }
}