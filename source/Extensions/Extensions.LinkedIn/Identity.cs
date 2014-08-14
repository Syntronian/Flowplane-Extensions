using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;
namespace Extensions.LinkedIn
{
    public class Identity : ExtensionsCore.IIdentity
    {
        public string Code
        {
            get { return "LINKEDIN"; }
        }

        public string Name
        {
            get { return "LinkedIn"; }
        }

        public string Type
        {
            get
            {
                return Definitions.ProcessObjectTypes.Event.ToString();
            }
        }

        public string Toolbox_ID
        {
            get { return "tool-event-linkedin"; }
        }

        public string Toolbox_Description
        {
            get { return "LinkedIn task"; }
        }

        public string Toolbox_CSS
        {
            get { return "toolbox-event-linkedin"; }
        }

        public string Toolbox_Drag_CSS
        {
            get { return "toolbox-event-linkedin-drag"; }
        }
    }
}
