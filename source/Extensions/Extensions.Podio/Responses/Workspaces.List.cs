using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio.Responses.Workspaces
{
    public class Item
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



    public class List
    {
        public List<Item> items { get; set; }

        public List(IEnumerable<PodioAPI.Models.Space> spaces)
        {
            this.items = new List<Item>();
            foreach (var space in spaces)
            {
                this.items.Add(new Item(space.SpaceId.ToString(), space.Name));
            }
        }
    }
}
