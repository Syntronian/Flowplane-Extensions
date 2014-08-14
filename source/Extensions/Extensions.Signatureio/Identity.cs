using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.Signatureio
{
    public class Identity : ExtensionsCore.IIdentity
    {
        public string Code
        {
            get { return "SIGNATUREIO"; }
        }

        public string Name
        {
            get { return "Signature.io"; }
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
            get { return "tool-activity-signatureio"; }
        }

        public string Toolbox_Description
        {
            get { return "Signature.io document"; }
        }

        public string Toolbox_CSS
        {
            get { return "toolbox-activity-signatureio"; }
        }

        public string Toolbox_Drag_CSS
        {
            get { return "toolbox-activity-signatureio-drag"; }
        }
    }
}
