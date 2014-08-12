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
    public class Facebook
    {
        private const string myAppId = "YOUR_APP_ID_HERE";
        private const string myAppToken = "YOUR_APP_TOKEN_HERE";
        private const string extId = "facebook";
        //[TestMethod]
        //public void ShareUpdate()
        //{
        //    var u = new FlowplaneExtensions.Controllers.api.Api_FlowController().ActivateObject(GetCol("My fist post: Using facebook web api's from flowplane"));
        //    Console.WriteLine(System.Net.HttpStatusCode.OK);
        //}
        private FormDataCollection GetCol(string message)
        {
            var ak = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "appId",value = myAppId },
               new  Models.api.FpxtParam { key = "appToken",value = myAppToken }
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
