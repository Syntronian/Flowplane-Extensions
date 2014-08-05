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
    }
}
