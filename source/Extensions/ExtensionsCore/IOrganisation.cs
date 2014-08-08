using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IOrganisation
    {
        string id { get; }
        string name { get; }
    }

    public interface IOrganisations
    {
        List<IOrganisation> items { get; set; }
    }
}
