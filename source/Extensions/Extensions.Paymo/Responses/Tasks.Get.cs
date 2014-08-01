using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Paymo.Responses.Tasks
{
    public class Get
    {
        private System.Xml.XmlDocument xml = new System.Xml.XmlDocument();

        public Get(string rs)
        {
            this.xml.LoadXml(rs);
            if (!this.xml.DocumentElement.HasAttributes) throw new Exception("Invalid response.");
            if (!this.xml.DocumentElement.Attributes[0].Value.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
                throw Auth.ParseError(rs);
        }

        public string id
        {
            get
            {
                var nd = this.xml.DocumentElement.FirstChild.Attributes[0];
                if (nd == null) return null;
                return nd.Value;
            }
        }

        public string name
        {
            get
            {
                var nd = this.xml.DocumentElement.FirstChild.FirstChild.FirstChild;
                if (nd == null) return null;
                return nd.InnerText;
            }
        }

        public string description
        {
            get
            {
                var nd = this.xml.DocumentElement.FirstChild.ChildNodes[1].FirstChild;
                if (nd == null) return null;
                return nd.InnerText;
            }
        }

        public bool complete
        {
            get
            {
                var nd = this.xml.DocumentElement.FirstChild.Attributes[1];
                if (nd == null) return false;
                return nd.Value == "1" ? true : false;
            }
        }
    }
}
