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
    public class Facebook
    {
        private const string myAppId = "YOUR_APP_ID_HERE";
        private const string myAppToken = "YOUR_APP_TOKEN_HERE";
        private const string extId = "facebook";
        [TestMethod]
        public void ShareUpdate()
        {
            var u = new FlowplaneExtensions.Controllers.api.Api_FlowController().Exec(GetCol("My fist post: Using facebook web api's from flowplane"));
            Console.WriteLine(System.Net.HttpStatusCode.OK);
        }

        private FormDataCollection GetCol(string message)
        {
            var pairs = new Dictionary<string, string> 
                { 
                    { "extId",  extId}, 
                    { "appId" , myAppId}, 
                    { "appToken" , myAppToken} , 
                    { "message" , message} 
                };
            return new FormDataCollection(pairs);
        }


    }
}
