using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IProject
    {
        string id { get; }
        string name { get; }
    }

    public interface IProjects
    {
        List<IProject> items { get; set; }
    }
}
