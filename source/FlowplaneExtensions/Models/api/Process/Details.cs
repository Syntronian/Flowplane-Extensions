﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Extensions.Asana;
using Extensions.Paymo;
using ExtensionsCore;

namespace FlowplaneExtensions.Models.api.Process
{
    public class Details
    {
        public IAssignees GetAssignees(string extId, string apiKey,string uName = null,string password=null)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Extensions.Asana.Users(new Extensions.Asana.Auth(apiKey)).List();
            }
            if (extId.Equals(new Extensions.Paymo.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            { 
                if(uName == null) throw new Exception("Paymo user name is mandatory.");
                if(password == null) throw new Exception("Paymo password is mandatory.");
                return new Extensions.Paymo.Users(new Extensions.Paymo.Auth(apiKey, uName, password)).List();
            }
            throw new Exception("Invalid extension.");
        }

        public Extensions.Asana.Responses.Workspaces.List GetWorkSpaces(string extId, string apiKey)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var ws = new Workspaces(new Auth(apiKey));
                return ws.List();
            }
            throw new Exception("Invalid extension.");
        }

        public Extensions.Asana.Responses.Projects.List GetProjects(string extId, string apiKey, string wsId, bool? archived = null)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var p = new Projects(new Auth(apiKey));
                return p.List(wsId, archived);
            }
            throw new Exception("Invalid extension.");
        }
    }
}