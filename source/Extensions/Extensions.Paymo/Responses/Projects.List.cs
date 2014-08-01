using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsCore;
namespace Extensions.Paymo.Responses.Projects
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
        private System.Xml.XmlDocument xml = new System.Xml.XmlDocument();

        public List<IProject> items { get; set; }

        public List(string rs)
        {
            this.items = new List<IProject>();
            this.xml.LoadXml(rs);
            if (!this.xml.DocumentElement.HasAttributes) throw new Exception("Invalid response.");
            if (!this.xml.DocumentElement.Attributes[0].Value.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
                throw Auth.ParseError(rs);

            foreach (System.Xml.XmlNode o in this.xml.DocumentElement.FirstChild.ChildNodes)
            {
                string id = null;
                string name = null;
                bool retired = false;

                var nd = o.Attributes[0]; if (nd != null) id = nd.InnerText;
                nd = o.Attributes[1];     if (nd != null) name = nd.InnerText;
                nd = o.Attributes[2]; if (nd != null) retired = nd.InnerText.Equals("1") ? true : false;

                if (!retired)
                    this.items.Add(new Item(id, name));
            }
        }
    }
}
