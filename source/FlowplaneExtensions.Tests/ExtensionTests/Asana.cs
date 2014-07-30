using System;
using Extensions.Asana;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlowplaneExtensions.Tests.ExtensionTests
{
    [TestClass]
    public class Asana
    {
        private const string myAPIKEY = "";

        private Extensions.Asana.Auth auth = new Extensions.Asana.Auth(myAPIKEY);

        [TestMethod]
        public void Login()
        {
            var auth = new Extensions.Asana.Auth(this.auth);
            Console.Write(auth.TestGet());
        }

        [TestMethod]
        public void ListWorkspaces()
        {
            var wss = new Extensions.Asana.Workspaces(this.auth);
            var rs = wss.List();
            foreach (var ws in rs.items)
            {
                Console.WriteLine("id: " + ws.id);
                Console.WriteLine("name: " + ws.name);
            }
        }

        [TestMethod]
        public void ListProjects()
        {
            var prj = new Extensions.Asana.Projects(this.auth);

            Console.WriteLine("List all");
            foreach (var p in prj.List().items)
            {
                Console.WriteLine("id: " + p.id);
                Console.WriteLine("name: " + p.name);
            }
        }

        [TestMethod]
        public void ListUsers()
        {
            var users = new Extensions.Asana.Users(this.auth);
            var rs = users.List();
            foreach (var user in rs.items)
            {
                Console.WriteLine("id: " + user.id);
                Console.WriteLine("name: " + user.name);
            }
        }
    }
}
