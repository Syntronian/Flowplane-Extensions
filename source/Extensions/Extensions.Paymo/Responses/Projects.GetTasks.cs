using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Paymo.Responses.Projects
{
    public class GetTasks
    {
        private System.Xml.XmlDocument xml = new System.Xml.XmlDocument();

        public class Task
        {
            private string id;
            private string name;
            private bool complete;

            public Task(string id, string name, string complete)
            {
                this.id = id;
                this.name = name;
                this.complete = complete == "1" ? true : false;
            }

            public string ID
            {
                get
                {
                    return this.id;
                }
                set
                {
                    this.id = value;
                }
            }

            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }

            public bool Completed
            {
                get
                {
                    return this.complete;
                }
                set
                {
                    this.complete = value;
                }
            }
        }

        public GetTasks(string rs)
        {
            this.xml.LoadXml(rs);
            if (!this.xml.DocumentElement.HasAttributes) throw new Exception("Invalid response.");
            if (!this.xml.DocumentElement.Attributes[0].Value.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
                throw Auth.ParseError(rs);
        }

        public List<Task> tasks
        {
            get
            {
                var ret = new List<Task>();
                foreach (System.Xml.XmlNode item in this.xml.DocumentElement.FirstChild.ChildNodes)
                {
                    if (item.Attributes.Count == 0) continue;
                    var ndID = item.Attributes[0]; if (ndID == null) continue;

                    var task = new Task(ndID.Value, item.Attributes[1].Value, item.Attributes[2].Value);
                    ret.Add(task);
                }
                return ret;
            }
        }
    }
}
