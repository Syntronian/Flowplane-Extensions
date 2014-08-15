using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.Podio
{
    public class Identity : ExtensionsCore.IIdentity
    {
        public string Code
        {
            get { return "PODIO"; }
        }

        public string Name
        {
            get { return "Podio"; }
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
            get { return "tool-activity-podio"; }
        }

        public string Toolbox_Description
        {
            get { return "Podio task"; }
        }

        public string Toolbox_CSS
        {
            get { return "toolbox-activity-podio"; }
        }

        public string Toolbox_Content
        {
            get { return ""; }
        }

        public string Toolbox_Drag_CSS
        {
            get { return "toolbox-activity-podio-drag"; }
        }
    }
}
