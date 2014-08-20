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
        //    var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetApps(GetCol(null,"YOUR_SPACE_ID_HERE"));
        //    Common.Display(o);
        //}

        //[TestMethod]
        //public void GetItems()
        //{
        //    var o = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetItems(GetCol(null,null,"YOUR_APP_ID_HERE"));
        //    Common.Display(o);
        //}

        //[TestMethod]
        //public void GetLoginUrl()
        //{
        //    var o = new FlowplaneExtensions.Controllers.api.Api_OAuthController().GetLoginUrl(GetCol(null,null,null,"YOUR_REDIRECT_URI_HERE"));
        //    Assert.IsTrue(o != null);
        //}

        //[TestMethod]
        //public void GetAccessToken()
        //{
        //    var o = new FlowplaneExtensions.Controllers.api.Api_OAuthController().GetAccessToken(GetCol(null,null,null,"YOUR_REDIRECT_URI_HERE", "YOUR_AUTH_CODE_HERE"));
        //    Assert.IsTrue(o != null);
        //}

        private FormDataCollection GetCol(string orgId = null, string spaceId = null, string appId = null, string redirectUri = null, string authCode = null)
        {
            var ak = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "ClientId",value = clientId },
               new  Models.api.FpxtParam { key = "ClientSecret",value = clientSecret },
               new  Models.api.FpxtParam { key = "AccessToken",value = accessToken },
               new  Models.api.FpxtParam { key = "OrgId",value = orgId}
            };
            var authKeys = JsonConvert.SerializeObject(ak);

            var obP = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "spaceId",value = spaceId},
               new  Models.api.FpxtParam { key = "appId",value = appId},
                new  Models.api.FpxtParam { key = "redirectUri",value = redirectUri},
               new  Models.api.FpxtParam { key = "code",value = authCode}
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
