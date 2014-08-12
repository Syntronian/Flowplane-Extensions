using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;

namespace Extensions.Paymo.Responses.TaskLists
{
    public class Item : ITask
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



    public class List : ITasks
    {
        private System.Xml.XmlDocument xml = new System.Xml.XmlDocument();

        public List<ITask> items { get; set; }

        public List(string rs)
        {
            this.items = new List<ITask>();
            this.xml.LoadXml(rs);
            if (!this.xml.DocumentElement.HasAttributes) throw new Exception("Invalid response.");
            if (!this.xml.DocumentElement.Attributes[0].Value.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
                throw Auth.ParseError(rs);

            foreach (System.Xml.XmlNode o in this.xml.DocumentElement.FirstChild.ChildNodes)
            {
                string id = null;
                string name = null;

                var nd = o.Attributes[0]; if (nd != null) id = nd.InnerText;
                nd = o.Attributes[1]; if (nd != null) name = nd.InnerText;

                this.items.Add(new Item(id, name));
            }
        }
    }
}
