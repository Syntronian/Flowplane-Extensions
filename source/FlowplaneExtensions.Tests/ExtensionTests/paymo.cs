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
        private const string myAPIKEY = "53828d2dda48075c9f35f94361ce3534";
        private const string uName = "vaishakhip@gmail.com";
        private const string pwd = "lion@123";

        [TestMethod]
        public void ListUsers()
        {
            var users = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetAssignees(GetCol());
            this.Display(users);
        }

        private void Display(dynamic res)
        {
            foreach (var i in res.items)
            {
                Console.WriteLine("id: " + i.id);
                Console.WriteLine("name: " + i.name);
            }
        }

        private FormDataCollection GetCol()
        {
            var pairs = new Dictionary<string, string> 
                { 
                    { "extId",  "paymo"}, 
                    { "apiKey" , myAPIKEY}, 
                    { "userName" , uName} , 
                    { "password" , pwd}  
                };
            return new FormDataCollection(pairs);
        }
    }
}
