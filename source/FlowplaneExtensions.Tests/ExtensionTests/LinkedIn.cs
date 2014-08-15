using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Formatting;
using System.Net.Http;
using Newtonsoft.Json;
namespace FlowplaneExtensions.Tests.ExtensionTests
{
    [TestClass]
    public class LinkedIn
    {
        private const string consumerKey = "YOUR_CONSUMER_KEY_HERE";
        private const string consumerSecret = "YOUR_CONSUMER_SECRET";
        private const string accessToken = "YOUR_ACCESS_TOKEN";
        private const string extId = "linkedin";

        [TestMethod]
        public void GetLoginUrl()
        {
            var o = new FlowplaneExtensions.Controllers.api.Api_OAuthController().GetLoginUrl(GetCol("YOUR_CALLBACK_URL_HERE"));
            Assert.IsTrue(o != null);
        }

        [TestMethod]
        public void GetAccessToken()
        {
            var o = new FlowplaneExtensions.Controllers.api.Api_OAuthController().GetAccessToken(GetCol("YOUR_CALLBACK_URL_HERE"));
            Assert.IsTrue(o != null);
        }

        [TestMethod]
        public void ShareUpdate()
        {
            var u = new FlowplaneExtensions.Controllers.api.Api_FlowController().ActivateObject(GetCol("YOUR_CALLBACK_URL_HERE", "My fist post: Using LinkedIn web api's from flowplane"));
            Console.WriteLine(System.Net.HttpStatusCode.OK);
        }

        private FormDataCollection GetCol(string callback, string accessToken = null, string message = null)
        {
            var ak = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "consumerKey",value = consumerKey },
               new  Models.api.FpxtParam { key = "consumerSecret",value = consumerSecret },
               new  Models.api.FpxtParam { key = "accessToken",value = accessToken },
               new  Models.api.FpxtParam { key = "callback",value = callback}
            };
            var authKeys = JsonConvert.SerializeObject(ak);

            var obP = new List<Models.api.FpxtParam>()
            {
                new  Models.api.FpxtParam { key = "message",value = message}
            };
            var objParams = JsonConvert.SerializeObject(obP);

            var finalCol = new Dictionary<string, string>
                { 
                    { "extId" , extId},
                    { "authKeys", authKeys},
                    {"objParams",objParams}
                };

            return new FormDataCollection(finalCol);
        }
    }
}
