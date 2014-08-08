using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public class AuthKey
    {
        public string key { get; set; }
        public string controlId { get; set; }
        public string controlDescription { get; set; }
        public bool required { get; set; }
    }
}
