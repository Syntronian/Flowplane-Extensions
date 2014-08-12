using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Extensions.Wrike.Responses.Tasks;
using System.Net.Http;
namespace Extensions.Wrike
{
    public class Tasks : Auth
    {
        public const string baseURL = "https://www.wrike.com/api/json/v2";

        public Tasks(Auth auth)
            : base(auth)
        {
        }

        public Info Get()
        {
            using (var client = new HttpClient())
            {
                var reqURL = this.GenerateUrl(baseURL + "/wrike.task.get");
                var response = client.GetStringAsync(reqURL).Result;
                return new Info(response);
            }
        }

        public Info Create(NameValueCollection addParams = null)
        {
            using (var client = new HttpClient())
            {
                var reqURL = this.GenerateUrl(baseURL + "/wrike.task.add", addParams);
                var response = client.GetStringAsync(reqURL).Result;
                return new Info(response);
            }
        }

        public Info Create()
        {
            return this.Create(null);
        }

        public Info Update(NameValueCollection addParams)
        {
            using (var client = new HttpClient())
            {
                var reqURL = this.GenerateUrl(baseURL + "/wrike.task.update",addParams);
                var response = client.GetStringAsync(reqURL).Result;
                return new Info(response);
            }
        }
    }
}
