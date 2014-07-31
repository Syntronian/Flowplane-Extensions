using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Extensions.Asana;
using ExtensionsCore;

namespace FlowplaneExtensions.Models.api.Process
{
    public class Details
    {
        public IAssignees GetAssignees(string extId, string apiKey)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var u = new Extensions.Asana.Users(new Auth(apiKey));
                return u.List();
            }
            throw new Exception("Invalid extension.");
        }

        public dynamic GetWorkSpaces(string extId, string apiKey)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var ws = new Workspaces(new Auth(apiKey));
                return ws.List();
            }
            throw new Exception("Invalid extension.");
        }

        public dynamic GetProjects(string extId, string apiKey, string wsId, bool? archived = null)
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