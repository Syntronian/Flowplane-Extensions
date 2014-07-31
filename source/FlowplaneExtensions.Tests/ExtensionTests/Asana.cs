using System;
using Extensions.Asana;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlowplaneExtensions;
using System.Net.Http.Formatting;
using System.Collections.Generic;

namespace FlowplaneExtensions.Tests.ExtensionTests
{
    [TestClass]
    public class Asana
    {
        private const string myAPIKEY = "2XaGT9Ig.5VGoJUHAQwFOmuIsT2izPLx";

        //[TestMethod]
        //public void Login()
        //{
        //    var auth = new Extensions.Asana.Auth(this.auth);
        //    Console.Write(auth.TestGet());
        //}

        [TestMethod]
        public void ListUsers()
        {
            var users = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetAssignees(GetCol());
            this.Display(users);
        }

        [TestMethod]
        public void ListWorkSpaces()
        {
            var ws = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetWorkSpaces(GetCol());
            this.Display(ws);
        }

        [TestMethod]
        public void ListProjects()
        {
            var p = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetProjects(GetCol());
            this.Display(p);
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
                    { "extId",  "asana"}, 
                    { "apiKey" , myAPIKEY} 
                };
            return new FormDataCollection(pairs);
        }
    }
}
