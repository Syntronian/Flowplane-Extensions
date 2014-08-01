using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;
namespace Extensions.Asana.Responses.Projects
{
    public class Item : IProject
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

    public class List : IProjects
    {
        private Newtonsoft.Json.Linq.JObject obj = null;

        public List<IProject> items { get; set; }

        public List(string json)
        {
            this.items = new List<IProject>();
            this.obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);
            foreach (var o in this.obj.First.First.ToList())
            {
                var item = new Item(o.Value<string>("id"),
                                    o.Value<string>("name"));
                this.items.Add(item);
            }
        }
    }
}
