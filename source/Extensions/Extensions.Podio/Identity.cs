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
    }
}
