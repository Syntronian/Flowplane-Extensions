using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Asana.Responses.Tasks
{
    public class Get
    {
        private Newtonsoft.Json.Linq.JObject obj = null;

        public Get(string json)
        {
            this.obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);
        }

        public string id
        {
            get
            {
                return this.obj.First.First.Value<string>("id");
            }
        }

        public string name
        {
            get
            {
                return this.obj.First.First.Value<string>("name");
            }
        }

        public bool completed
        {
            get
            {
                return this.obj.First.First.Value<bool>("completed");
            }
        }
    }
}
