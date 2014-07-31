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

        private Extensions.Asana.Auth auth = new Extensions.Asana.Auth(myAPIKEY);

        //[TestMethod]
        //public void Login()
        //{
        //    var auth = new Extensions.Asana.Auth(this.auth);
        //    Console.Write(auth.TestGet());
        //}

        //[TestMethod]
        //public void ListWorkspaces()
        //{
        //    var wss = new Extensions.Asana.Workspaces(this.auth);
        //    var rs = wss.List();
        //    foreach (var ws in rs.items)
        //    {
        //        Console.WriteLine("id: " + ws.id);
        //        Console.WriteLine("name: " + ws.name);
        //    }
        //}

        //[TestMethod]
        //public void ListProjects()
        //{
        //    var prj = new Extensions.Asana.Projects(this.auth);

        //    Console.WriteLine("List all");
        //    foreach (var p in prj.List().items)
        //    {
        //        Console.WriteLine("id: " + p.id);
        //        Console.WriteLine("name: " + p.name);
        //    }
        //}

        [TestMethod]
        public void ListUsers()
        {
            var controller = new FlowplaneExtensions.Controllers.api.Api_ProcessController();
            var pairs = new Dictionary<string,string> 
            { 
                { "extId",  "asana"}, 
                { "apiKey" , myAPIKEY} 
            };
            var fd = new FormDataCollection(pairs);
            var users = controller.GetAssignees(fd);
            //var users = new Extensions.Asana.Users(this.auth);
            foreach (var user in users.items)
            {
                Console.WriteLine("id: " + user.id);
                Console.WriteLine("name: " + user.name);
            }
        }
    }
}
