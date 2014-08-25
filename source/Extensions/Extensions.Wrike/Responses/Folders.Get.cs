using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using ExtensionsCore;
namespace Extensions.Wrike.Responses.Folders
{
    public class Get : ITree
    {
        public FoldersTreeDTO treeDTO { get; set; }

        public ITreeRoot tree
        {
            get
            {
                return this.treeDTO;
            }

        }

        public Get(string result)
        {
            this.treeDTO = new JavaScriptSerializer().Deserialize<FoldersTreeDTO>(result);
            if (this.treeDTO.foldersTree == null)
            {
                JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                if (obj.First.First.Value<string>("code") != null)
                    throw new Exception(obj.First.First.Value<string>("code") + " : " + obj.First.Last.Value<string>("message"));
            }
        }

        public class Folders : ITreeParent
        {
            public ITreeKids[] folders { get; set; }
        }

        public class Folder : ITreeKids
        {
            public string id { get; set; }
            public string parentId { get; set; }
            public string title { get; set; }
        }

        public class FoldersTreeDTO : ITreeRoot
        {
            public ITreeParent foldersTree { get; set; }
        }
    }
}
