using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IWorkspace
    {
        string id { get; }
        string name { get; }
    }

    public interface IWorkspaces
    {
        List<IWorkspace> items { get; set; }
    }
}
