using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IIdentity
    {
        string Code { get; }
        string Name { get; }
        string Type { get; }
        string Toolbox_ID { get; }

        string Toolbox_Description { get; }

        string Toolbox_CSS { get; }

        string Toolbox_Content { get; }

        string Toolbox_Drag_CSS { get; }
    }
}
