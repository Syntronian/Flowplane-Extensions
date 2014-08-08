using System;
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
    }
}
