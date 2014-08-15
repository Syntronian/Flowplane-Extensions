using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.Asana
{
    public class Identity : ExtensionsCore.IIdentity
    {
        public string Code
        {
            get { return "ASANA"; }
        }

        public string Name
        {
            get { return "Asana"; }
        }

        public string Type
        {
            get
            {
                return Definitions.ProcessObjectTypes.Activity.ToString();
            }
        }

        public string Toolbox_ID
        {
            get { return "tool-activity-asana"; }
        }

        public string Toolbox_Description
        {
            get { return "Asana task"; }
        }

        public string Toolbox_CSS
        {
            get { return "toolbox-activity-asana"; }
        }

        public string Toolbox_Content
        {
            get { return ""; }
        }

        public string Toolbox_Drag_CSS
        {
            get { return "toolbox-activity-asana-drag"; }
        }
    }
}
