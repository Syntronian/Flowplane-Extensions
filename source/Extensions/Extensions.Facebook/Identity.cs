using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.Facebook
{
    public class Identity : ExtensionsCore.IIdentity
    {
        public string Code
        {
            get { return "FACEBOOK"; }
        }

        public string Name
        {
            get { return "Facebook"; }
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
            get { return "tool-event-facebook"; }
        }

        public string Toolbox_Description
        {
            get { return "Facebook event"; }
        }

        public string Toolbox_CSS
        {
            get { return "toolbox-event-facebook"; }
        }

        public string Toolbox_Content
        {
            get { return "<i class=\"fa-facebook-square fa fa-2x toolbox-event-facebook-f\"></i>"; }
        }

        public string Toolbox_Drag_CSS
        {
            get { return "toolbox-event-facebook-drag"; }
        }
    }
}
