using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;

namespace Extensions.Asana
{
    public class Auth
    {
        private string apiKey;
        private HttpClient httpClient = new HttpClient();

        public Auth()
        {
        }

        public Auth(Auth auth)
        {
            this.API_Key = auth.API_Key;
            this.httpClient = auth.httpClient;
        }

        public Auth(string apiKey)
        {
            this.apiKey = apiKey;

            // set up authorisation header
            this.httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                                                                      Convert.ToBase64String(Encoding.ASCII.GetBytes(apiKey + ":"))
                                                                     );
        }

        public string API_Key
        {
            get
            {
                return this.apiKey;
            }
            set
            {
                this.apiKey = value;
            }
        }

        public HttpClient HttpClient
        {
            get
            {
                return this.httpClient;
            }
        }

        public string TestGet()
        {
            return this.httpClient.GetStringAsync("https://app.asana.com/api/1.0/users/me").Result;
        }
    }
}
