using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions.Wrike.Responses.Users;
using System.Net.Http;
namespace Extensions.Wrike
{
    public class Users : Auth
    {
        public const string baseURL = "https://www.wrike.com/api/json/v2/wrike.contacts.list";

        public Users(Auth auth) : base(auth)
        {
        }

        public Get Get()
        {
            using (var client = new HttpClient())
            {
                var reqURL = this.GenerateUrl(baseURL);
                var response = client.GetStringAsync(reqURL).Result;
                return new Get(response);
            }
        }
    }
}
