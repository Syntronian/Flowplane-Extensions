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
    public class Wrike
    {
        private const string consumerKey = "YOUR_CONSUMER_KEY_HERE";
        private const string consumerSecret = "YOUR_CONSUMER_SECRET_HERE";
        private const string accessToken = "YOUR_ACCESS_TOKEN_HERE";
        private const string accessTokenSecret = "YOUR_ACCESS_TOKEN_SECRET_HERE";
        private const string callback = "YOUR_CALLBACK_URL_HERE";

        //[TestMethod]
        //public void GetAssignees()
        //{
        //    var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetAssignees(GetCol());
        //    Common.Display(o);
        //}

        [TestMethod]
        public void GetFolders()
        {
            //var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetFolders(GetCol());
            //Common.Display(o);
        }

        private FormDataCollection GetCol()
        {
            var ak = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "ConsumerKey",value = consumerKey },
               new  Models.api.FpxtParam { key = "ConsumerSecret",value = consumerSecret },
               new  Models.api.FpxtParam { key = "AccessToken",value = accessToken },
               new  Models.api.FpxtParam { key = "AccessTokenSecret",value = accessTokenSecret },
                new  Models.api.FpxtParam { key = "callback",value = callback}
            };
            var authKeys = JsonConvert.SerializeObject(ak);


            var finalCol = new Dictionary<string, string>
                { 
                    { "extId" , "wrike"},
                    { "authKeys", authKeys},
                    {"objParams",null}
                };

            return new FormDataCollection(finalCol);
        }


    }
}
