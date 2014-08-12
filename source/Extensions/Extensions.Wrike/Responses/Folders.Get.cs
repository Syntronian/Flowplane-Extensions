using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Extensions.Wrike.Responses.Folders
{
    public class Get
    {
        public FoldersTreeDTO tree { get; set; }

        public Get(string result)
        {
            this.tree = new JavaScriptSerializer().Deserialize<FoldersTreeDTO>(result);
            if (this.tree == null)
            { 
                JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                if(obj.First.First.Value<string>("code") != null)
                    throw new Exception(obj.First.First.Value<string>("code") + " : " + obj.First.Last.Value<string>("message"));
            }
        }

        public class Folders
        {
            public Folder[] folders { get; set; }
        }

        public class Folder
        {
            public string id { get; set; }
            public string parentId { get; set; }
            public string title { get; set; }
        }

        public class FoldersTreeDTO
        {
            public Folders foldersTree { get; set; }
        }
    }
}
