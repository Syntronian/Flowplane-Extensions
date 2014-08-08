using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Formatting;

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
        //    var users = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetAssignees(GetCol());
        //    Common.Display(users);
        //}

        //[TestMethod]
        //public void ListProjects()
        //{
        //    var users = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetProjects(GetCol());
        //    Common.Display(users);
        //}

        private FormDataCollection GetCol()
        {
            var pairs = new Dictionary<string, string> 
                { 
                    { "extId",  "paymo"}, 
                    { "apiKey" , myAPIKEY}, 
                    { "userName" , uName} , 
                    { "password" , pwd} ,
                    {"authToken", token}
                };
            return new FormDataCollection(pairs);
        }
    }
}
