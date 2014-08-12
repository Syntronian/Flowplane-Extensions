using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ExtensionsCore;

namespace Extensions.Wrike.Responses.Users
{
    public class User : ExtensionsCore.IAssignee
    {
        public string id { get; set; }
        public string name { get; set; }
    }


    public class Get : ExtensionsCore.IAssignees
    {
        private JObject obj = null;

        public List<ExtensionsCore.IAssignee> items { get; set; }

        public Get(string result)
        {
            this.obj = JsonConvert.DeserializeObject<JObject>(result);

            if (this.obj.First.First.Value<string>("code") != null)
                throw new Exception(this.obj.First.First.Value<string>("code") + " : " + this.obj.First.Last.Value<string>("message"));

            this.items = new List<ExtensionsCore.IAssignee>();
            foreach (var o in this.obj.First.First.First.First.ToList())
            {
                this.items.Add(new User
                {
                    id = o.Value<string>("uid"),
                    name = o.Value<string>("firstName")
                });
            }
        }
    }
}
