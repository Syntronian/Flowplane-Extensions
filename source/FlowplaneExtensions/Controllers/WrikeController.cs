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
    public class WrikeController : Controller
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
        public ActionResult oauth(string txtWrikeConsumerKey, string txtWrikeConsumerSecret)
        {
            TempData["txtWrikeConsumerKey"] = txtWrikeConsumerKey;
            TempData["txtWrikeConsumerSecret"] = txtWrikeConsumerSecret;

            if (string.IsNullOrEmpty(txtWrikeConsumerKey) && string.IsNullOrEmpty(txtWrikeConsumerSecret))
            {
                // use flowplane
                var httpClient = new HttpClient();
                var rs = httpClient.GetAsync(
                    string.Format(Common.FlowplaneDotCom + "/api/oauth/getloginurl/{0}?returnurl={1}",
                                  new Extensions.Wrike.Identity().Code,
                                  Request.Url.AbsoluteUri)).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

                var tokens = JsonConvert.DeserializeObject<string>(rs.Content.ReadAsStringAsync().Result);

                var split = tokens.Split('&');
                TempData["WrikeRequestTokenSecret"] = split[1].Substring(split[1].IndexOf("=") + 1);

                return Redirect(string.Format("https://www.wrike.com/rest/auth/authorize?{0}", tokens));
            }
            else
            {
                var auth = new Extensions.Wrike.Tokens(new Extensions.Wrike.Auth(txtWrikeConsumerKey, txtWrikeConsumerSecret, null, null, Request.Url.AbsoluteUri));
                var tokens = auth.GetRequestToken();
                var split = tokens.Split('&');
                TempData["WrikeRequestTokenSecret"] = split[1].Substring(split[1].IndexOf("=") + 1);

                return Redirect(string.Format("https://www.wrike.com/rest/auth/authorize?{0}", tokens));
            }
        }

        public ActionResult oauth()
        {
            if (Request["oauth_token"] == null)
                throw new Exception("Invalid auth token.");

            var txtWrikeConsumerKey = TempData["txtWrikeConsumerKey"].ToString();
            var txtWrikeConsumerSecret = TempData["txtWrikeConsumerSecret"].ToString();
            var redirectUri = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string[] tokens;
            if (string.IsNullOrEmpty(txtWrikeConsumerKey) && string.IsNullOrEmpty(txtWrikeConsumerSecret))
            {
                // use flowplane
                var httpClient = new HttpClient();
                var rs = httpClient.GetAsync(
                    string.Format(Common.FlowplaneDotCom + "/api/oauth/getaccesstokens/{0}?wrikeauthtoken={1}&wrikeauthtokensecret={2}&returnurl={3}",
                                  new Extensions.Wrike.Identity().Code,
                                  Request["oauth_token"],
                                  TempData.Peek("WrikeRequestTokenSecret").ToString(),
                                  redirectUri)).Result;
                if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

                tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(rs.Content.ReadAsStringAsync().Result)["AccessToken"].Split('&');
            }
            else
            {
                var auth = new Extensions.Wrike.Tokens(new Extensions.Wrike.Auth(txtWrikeConsumerKey, txtWrikeConsumerSecret, Request["oauth_token"], TempData.Peek("WrikeRequestTokenSecret").ToString(), redirectUri));
                tokens = auth.GetAccessToken().Split('&');
            }

            var oauth_token = tokens[0].Substring(tokens[0].IndexOf("=") + 1);
            var oauth_token_secret = tokens[1].Substring(tokens[1].IndexOf("=") + 1);

            ViewBag.WrikeAccessToken = oauth_token;
            ViewBag.WrikeAccessTokenSecret = oauth_token_secret;

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