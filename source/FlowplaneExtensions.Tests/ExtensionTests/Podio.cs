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
    public class Podio
    {
        private const string clientId = "YOUR_CLIENTID_HERE";
        private const string clientSecret = "YOUR_CLIENT_SECRET_HERE";
        private const string accessToken = "YOUR_ACCESS_TOKEN_HERE";
        //[TestMethod]
        //public void GetOrg()
        //{
        //    var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetOrganisations(GetCol());
        //    Common.Display(o);
        //}

        //[TestMethod]
        //public void GetAssignees()
        //{
        //    var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetAssignees(GetCol("YOUR_ORGANISATION_ID_HERE"));
        //    Common.Display(o);
        //}
        //[TestMethod]
        //public void GetWorkSpaces()
        //{
        //    var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetWorkSpaces(GetCol("YOUR_ORGANISATION_ID_HERE"));
        //    Common.Display(o);
        //}

        //[TestMethod]
        //public void GetApps()
        //{
        //    var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetApps(GetCol("YOUR_SPACE_ID_HERE"));
        //    Common.Display(o);
        //}

        //[TestMethod]
        //public void GetItems()
        //{
        //    var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetItems(GetCol("YOUR_APP_ID_HERE"));
        //    Common.Display(o);
        //}
        private FormDataCollection GetCol(string orgId = null, string spaceId = null, string appId = null)
        {
            var ak = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "clientId",value = clientId },
               new  Models.api.FpxtParam { key = "clientSecret",value = clientSecret },
               new  Models.api.FpxtParam { key = "accessToken",value = accessToken }
            };
            var authKeys = JsonConvert.SerializeObject(ak);

            var obP = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "organisationId",value = orgId},
               new  Models.api.FpxtParam { key = "spaceId",value = spaceId},
               new  Models.api.FpxtParam { key = "appId",value = appId}
            };
            var objParams = JsonConvert.SerializeObject(obP);

            var finalCol = new Dictionary<string, string>
                { 
                    { "extId" , "podio"},
                    { "authKeys", authKeys},
                    {"objParams",objParams}
                };

            return new FormDataCollection(finalCol);
        }
    }
}
