using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IItem
    {
        string id { get; }
        string name { get; }
    }

    public interface IItems
    {
        List<IItem> items { get; set; }
    }
}
