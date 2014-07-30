using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IIdentity
    {
        string Code { get; }
        string Name { get; }
    }
}
