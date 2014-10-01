using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using FlowplaneExtensions.Models.api.Flow;
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
                                  new Extensions.Podio.Identity().Code,
                                  Request.Url.AbsoluteUri)).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

                var url = JsonConvert.DeserializeObject<string>(rs.Content.ReadAsStringAsync().Result);

                return Redirect(HttpUtility.UrlDecode(url));
            }
            else
            {
                var auth = new Extensions.Podio.Auth(txtPodioAppId, txtPodioAppSecret);
                return Redirect(HttpUtility.UrlDecode(auth.GetLoginUrl(Request.Url.AbsoluteUri)));
            }
        }
        
        public ActionResult oauth()
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
                    string.Format(Common.FlowplaneDotCom + "/api/oauth/getaccesstokens/{0}?podiocode={1}&returnurl={2}",
                                  new Extensions.Podio.Identity().Code,
                                  Request["code"],
                                  Request.Url.AbsoluteUri)).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);
                ViewBag.AuthCode = Request["code"];
                ViewBag.AccessToken = JsonConvert.DeserializeObject<Dictionary<string, string>>(rs.Content.ReadAsStringAsync().Result)["AccessToken"];
            }
            else
            {
                var auth = new Extensions.Podio.Auth(txtPodioAppId, txtPodioAppSecret);
                ViewBag.AccessToken = auth.GetAccessToken(Request["code"], Request.Url.AbsoluteUri);
            }

            return View("OAuthComplete");
        }

        public ActionResult FlowDetail(string fpxtpms)
        {
            if (string.IsNullOrEmpty(fpxtpms)) return View();

            ViewBag.pms = fpxtpms;
            return View(JsonConvert.DeserializeObject<Detail>(System.Uri.UnescapeDataString(fpxtpms)));
        }
    }
}