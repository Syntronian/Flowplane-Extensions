using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Paymo.Responses.Tasks
{
    public class Update
    {
        private System.Xml.XmlDocument xml = new System.Xml.XmlDocument();

        public Update(string rs)
        {
            this.xml.LoadXml(rs);
            if (!this.xml.DocumentElement.HasAttributes) throw new Exception("Invalid response.");
            if (!this.xml.DocumentElement.Attributes[0].Value.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
                throw Auth.ParseError(rs);
        }
    }
}
