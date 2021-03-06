﻿using System;
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
    public class Twitter
    {
        private const string consumerKey = "YOUR_CONSUMER_KEY_HERE";
        private const string consumerSecret = "YOUR_CONSUMER_SECRET";
        private const string accessToken = "YOUR_ACCESS_TOKEN";
        private const string accessSecret = "YOUR_ACCESS_TOKEN_SECRET";
        private const string extId = "twitter";

        //[TestMethod]
        //public void ShareUpdate()
        //{
        //    var u = new FlowplaneExtensions.Controllers.api.Api_FlowController().ActivateObject(GetCol("My fist post: Using Twitter web api's from flowplane"));
        //    Console.WriteLine(System.Net.HttpStatusCode.OK);
        //}
        private FormDataCollection GetCol(string message)
        {
            var ak = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "consumerKey",value = consumerKey },
               new  Models.api.FpxtParam { key = "consumerSecret",value = consumerSecret },
               new  Models.api.FpxtParam { key = "accessToken",value = accessToken },
               new  Models.api.FpxtParam { key = "accessTokenSecret",value = accessSecret }
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
