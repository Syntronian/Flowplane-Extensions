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

        public Definitions.ProcessObjectTypes Type
        {
            get
            {
                return Definitions.ProcessObjectTypes.Activity;
            }
        }
    }
}
