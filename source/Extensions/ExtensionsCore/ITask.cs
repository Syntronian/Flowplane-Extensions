using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface ITask
    {
        string id { get; }
        string name { get; }
    }

    public interface ITasks
    {
        List<ITask> items { get; set; }
    }
}
