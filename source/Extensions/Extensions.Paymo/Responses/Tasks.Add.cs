using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Paymo.Responses.Tasks
{
    public class Add
    {
        private System.Xml.XmlDocument xml = new System.Xml.XmlDocument();

        public Add(string rs)
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
                return nd.InnerText;
            }
        }
    }
}
