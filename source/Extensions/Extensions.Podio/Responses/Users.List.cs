using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio.Responses.Users
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



    public class List
    {
        public List<ExtensionsCore.IAssignee> items { get; set; }

        public List(IEnumerable<PodioAPI.Models.OrganizationMember> users)
        {
            this.items = new List<ExtensionsCore.IAssignee>();
            foreach (var user in users)
            {
                this.items.Add(new Item(user.Profile.UserId.ToString(), user.Profile.Name));
            }
        }
    }
}
