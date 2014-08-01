using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Paymo.Responses.Users
{
    public class Item : ExtensionsCore.IAssignee
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



    public class List : ExtensionsCore.IAssignees
    {
        private System.Xml.XmlDocument xml = new System.Xml.XmlDocument();

        public List<ExtensionsCore.IAssignee> items { get; set; }

        public List(string rs)
        {
            this.items = new List<ExtensionsCore.IAssignee>();
            this.xml.LoadXml(rs);
            if (!this.xml.DocumentElement.HasAttributes) throw new Exception("Invalid response.");
            if (!this.xml.DocumentElement.Attributes[0].Value.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
                throw Auth.ParseError(rs);

            foreach (System.Xml.XmlNode o in this.xml.DocumentElement.FirstChild.ChildNodes)
            {
                string id = null;
                //string username = null;
                string realname = null;
                bool active = false;

                var nd = o.Attributes[0]; if (nd != null) id = nd.InnerText;
                //nd = o.Attributes[1];     if (nd != null) username = nd.InnerText;
                nd = o.Attributes[2];     if (nd != null) realname = nd.InnerText;
                nd = o.Attributes[3];     if (nd != null) active = nd.InnerText.Equals("1") ? true : false;

                if (active)
                    this.items.Add(new Item(id, realname));
            }
        }
    }
}
