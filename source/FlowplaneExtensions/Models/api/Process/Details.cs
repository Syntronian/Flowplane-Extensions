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
        public IAssignees GetAssignees(string extId)
        {
            if (extId.Equals(new Extensions.Asana.Identity().Code, StringComparison.CurrentCultureIgnoreCase))
            {
                var u = new Extensions.Asana.Users(new Auth("2XaGT9Ig.5VGoJUHAQwFOmuIsT2izPLx"));
                return u.List();
            }
            throw new Exception("Invalid extension.");
        }
    }
}