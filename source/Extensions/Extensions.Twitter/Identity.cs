﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.Twitter
{
    public class Identity : ExtensionsCore.IIdentity
    {
        public string Code
        {
            get { return "TWITTER"; }
        }

        public string Name
        {
            get { return "Twitter"; }
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
            get { return "tool-event-twitter"; }
        }

        public string Toolbox_Description
        {
            get { return "Twitter event"; }
        }

        public string Toolbox_CSS
        {
            get { return "toolbox-event-twitter"; }
        }

        public string Toolbox_Content
        {
            get { return "<i class=\"fa-twitter fa fa-2x toolbox-event-twitter-bird\"></i>"; }
        }

        public string Toolbox_Drag_CSS
        {
            get { return "toolbox-event-twitter-drag"; }
        }
    }
}
