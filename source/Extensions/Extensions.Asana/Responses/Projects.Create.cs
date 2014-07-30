using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Asana.Responses.Projects
{
    public class Create
    {
        private Newtonsoft.Json.Linq.JObject obj = null;

        public Create(string json)
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

        public string notes
        {
            get
            {
                return this.obj.First.First.Value<string>("notes");
            }
        }
    }
}
