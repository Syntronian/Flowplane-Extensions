using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Wrike.Responses.Tasks
{
    public class Info
    {
        private Newtonsoft.Json.Linq.JObject obj = null;

        public Info(string result)
        {
            this.obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(result);
            if (this.id == null)
                throw new Exception(this.errorCode + " : " + this.errorMessage);
        }

        public string id
        {
            get
            {
                return this.obj.First.First.Value<string>("id");
            }
        }

        public string title
        {
            get
            {
                return this.obj.First.First.Value<string>("title");
            }
        }

        public List<String> ResponsibleUsers
        {
            get
            {
                var ret = new List<string>();

                foreach (var o in this.obj.First.ToList())
                {
                    ret.Add(o.Value<string>("responsibleUsers"));
                }
                return ret;
            }
        }

        public int status
        {
            get
            {
                return this.obj.First.First.Value<int>("status");
            }
        }

        public string errorCode
        {
            get
            {

                return this.obj.First.First.Value<string>("code");
            }
        }

        public string errorMessage
        {
            get
            {
                return this.obj.First.Last.Value<string>("message");
            }
        }
    }
}
