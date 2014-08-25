using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface ITreeKids
    {
        string id { get; }
        string parentId { get; }
        string title { get; }
    }

    public interface ITreeParent
    {
        ITreeKids[] folders { get; }
    }

    public interface ITreeRoot
    {
        ITreeParent foldersTree { get; }
    }

    public interface ITree
    {
        ITreeRoot tree { get; }
    }
}
