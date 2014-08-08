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
        private const string myAPIKEY = "YOUR_API_KEY_HERE";

        //[TestMethod]
        //public void ListUsers()
        //{
        //    var users = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetAssignees(GetCol());
        //    Common.Display(users);
        //}

        //[TestMethod]
        //public void ListWorkSpaces()
        //{
        //    var ws = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetWorkSpaces(GetCol());
        //    Common.Display(ws);
        //}

        //[TestMethod]
        //public void ListProjects()
        //{
        //    var p = new FlowplaneExtensions.Controllers.api.Api_ProcessController().GetProjects(GetCol());
        //    Common.Display(p);
        //}

        //private FormDataCollection GetCol()
        //{
        //    var pairs = new Dictionary<string, string> 
        //        { 
        //            { "extId",  "asana"}, 
        //            { "apiKey" , myAPIKEY} 
        //        };
        //    return new FormDataCollection(pairs);
        //}
    }
}
