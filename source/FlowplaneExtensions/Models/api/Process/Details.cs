using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Extensions.Asana;

namespace FlowplaneExtensions.Models.api.Process
{
    public class Details
    {
        public dynamic GetAssignees(string func)
        {
            if (func.Equals("asana", StringComparison.CurrentCultureIgnoreCase))
            {
                var u = new Extensions.Asana.Users(new Auth("2XaGT9Ig.5VGoJUHAQwFOmuIsT2izPLx"));
                return u.List();
            }
            throw new Exception("Invalid extension.");
        }
    }
}