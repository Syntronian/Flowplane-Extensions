using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IApp
    {
        string id { get; }
        string name { get; }
    }

    public interface IApps
    {
        List<IApp> items { get; set; }
    }
}
