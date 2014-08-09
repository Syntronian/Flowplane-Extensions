using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Formatting;
using System.Net.Http;

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
            var pairs = new Dictionary<string, string> 
                { 
                    { "extId",  extId}, 
                    { "consumerKey" , consumerKey}, 
                    { "consumerSecret" , consumerSecret} , 
                    { "accessToken" , accessToken} , 
                    { "accessTokenSecret" , accessSecret} , 
                    { "message" , message} 
                };
            return new FormDataCollection(pairs);
        }
    }
}
