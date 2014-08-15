using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.Wrike
{
    public class Identity : ExtensionsCore.IIdentity
    {
        public string Code
        {
            get { return "WRIKE"; }
        }

        public string Name
        {
            get { return "Wrike"; }
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
            get { return "tool-activity-wrike"; }
        }

        public string Toolbox_Description
        {
            get { return "Wrike task"; }
        }

        public string Toolbox_CSS
        {
            get { return "toolbox-activity-wrike"; }
        }

        public string Toolbox_Content
        {
            get { return ""; }
        }

        public string Toolbox_Drag_CSS
        {
            get { return "toolbox-activity-wrike-drag"; }
        }
    }
}
