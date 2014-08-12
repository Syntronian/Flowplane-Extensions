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
    public class Podio
    {
        private const string clientId = "YOUR_CLIENTID_HERE";
        private const string clientSecret = "YOUR_CLIENT_SECRET_HERE";
        private const string accessToken = "YOUR_ACCESS_TOKEN_HERE";
        [TestMethod]
        public void GetOrg()
        {
            var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetOrganisations(GetCol(null));
            Common.Display(o);
        }

        [TestMethod]
        public void GetAssignees()
        {
            var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetAssignees(GetCol("YOUR_ORGANISATION_ID_HERE"));
            Common.Display(o);
        }
        [TestMethod]
        public void GetWorkSpaces()
        {
            var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetWorkSpaces(GetCol("YOUR_ORGANISATION_ID_HERE"));
            Common.Display(o);
        }

        private FormDataCollection GetCol(string orgId = null)
        {
            var pairs = new Dictionary<string, string> 
                { 
                    { "extId",  "podio"}, 
                    { "clientId" , clientId } ,
                    { "clientSecret" , clientSecret } ,
                    { "accessToken" , accessToken} ,
                    {"organisationId", orgId}
                };
            return new FormDataCollection(pairs);
        }
    }
}
