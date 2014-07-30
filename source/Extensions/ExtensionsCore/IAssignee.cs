using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IAssignee
    {
        string id { get; }
        string name { get; }
    }

    public interface IAssignees
    {
        List<IAssignee> items { get; set; }
    }
}
