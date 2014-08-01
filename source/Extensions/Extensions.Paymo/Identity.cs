using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.Paymo
{
    public class Identity : ExtensionsCore.IIdentity
    {
        public string Code
        {
            get { return "PAYMO"; }
        }

        public string Name
        {
            get { return "Paymo"; }
        }

        public string Toolbox_ID
        {
            get { return "tool-activity-paymo"; }
        }
        
        public string Toolbox_Description
        {
            get { return "Paymo task"; }
        }

        public string Toolbox_CSS
        {
            get { return "toolbox-activity-paymo"; }
        }

        public string Toolbox_Drag_CSS
        {
            get { return "toolbox-activity-paymo-drag"; }
        }

        public string Type
        {
            get
            {
                return Definitions.ProcessObjectTypes.Activity.ToString();
            }
        }
    }
}
