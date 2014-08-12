using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net.Http;
namespace Extensions.Wrike
{
    public class Tokens : Auth
    {
        public const string requestTokenURL = "https://www.wrike.com/rest/auth/request_token";
        public const string accessTokenURL = "https://www.wrike.com/rest/auth/access_token";

        public Tokens(Auth auth) : base(auth)
        {
        }

        public string GetRequestToken()
        {
            using (var client = new HttpClient())
            {
                var reqURL = this.GenerateUrl(requestTokenURL);
                var response = client.GetStringAsync(reqURL).Result;
                return response;
            }
        }

        public string GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                var reqURL = this.GenerateUrl(accessTokenURL);
                var response = client.GetStringAsync(reqURL).Result;
                return response;
            }
        }
    }
}
