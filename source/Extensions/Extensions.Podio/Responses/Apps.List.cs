using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.Podio.Responses.Apps
{
    public class Item : IApp
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



    public class List : IApps
    {
        public List<IApp> items { get; set; }

        public List(IEnumerable<PodioAPI.Models.Application> apps)
        {
            this.items = new List<IApp>();
            foreach (var app in apps)
            {
                this.items.Add(new Item(app.AppId.ToString(), app.Config.Name));
            }
        }
    }
}
