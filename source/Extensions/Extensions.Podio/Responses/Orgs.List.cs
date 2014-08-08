using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;
namespace Extensions.Podio.Responses.Orgs
{
    public class Item : IOrganisation
    {
        public Item()
        {
        }

        public Item(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public string id { get; set; }
        public string name { get; set; }
    }



    public class List : IOrganisations
    {
        public List<IOrganisation> items { get; set; }

        public List(IEnumerable<PodioAPI.Models.Organization> orgs)
        {
            this.items = new List<IOrganisation>();
            foreach (var org in orgs)
            {
                this.items.Add(new Item(org.OrgId.ToString(), org.Name));
            }
        }
    }
}
