using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
namespace FlowplaneExtensions.Tests.ExtensionTests
{
    [TestClass]
    public class paymo
    {
        private const string myAPIKEY = "Replace_With_Your_API_Key";
        private const string uName = "Replace_With_Your_UserName";
        private const string pwd = "Replace_With_Your_Password";
        private const string token = "Replace_With_Your_Token";
        //[TestMethod]
        //public void ListUsers()
        //{
        //    var users = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetAssignees(GetCol(null));
        //    Common.Display(users);
        //}

        //[TestMethod]
        //public void ListProjects()
        //{
        //    var users = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetProjects(GetCol(null));
        //    Common.Display(users);
        //}

        //[TestMethod]
        //public void ListTasks()
        //{
        //    var users = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetTasks(GetCol("YOUR_PROJECT_ID_HERE"));
        //    Common.Display(users);
        //}
        private FormDataCollection GetCol(string projectId = null)
        {
            var ak = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "apiKey",value = myAPIKEY },
               new  Models.api.FpxtParam { key = "userName",value = uName },
               new  Models.api.FpxtParam { key = "password",value = pwd },
               new  Models.api.FpxtParam { key = "authToken",value = token }
            };
            var authKeys = JsonConvert.SerializeObject(ak);

            var obP = new List<Models.api.FpxtParam>()
            {
               new  Models.api.FpxtParam { key = "projectId",value = projectId}
            };
            var objParams = JsonConvert.SerializeObject(obP);

            var finalCol = new Dictionary<string, string>
                { 
                    { "extId" , "paymo"},
                    { "authKeys", authKeys},
                    {"objParams",objParams}
                };

            return new FormDataCollection(finalCol);
        }
    }
}
