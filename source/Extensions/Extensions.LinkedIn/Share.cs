using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
namespace Extensions.LinkedIn
{
    public class Share : Auth
    {
        public const string shareURL = "https://api.linkedin.com/v1/people/~/shares";

        public Share(Auth auth) : base(auth)
        { 
        }

        public string LinkedInShare(string comment)
        {
            using (HttpClient client = new HttpClient())
            {

                Uri uri = new Uri(shareURL + "?" + Parameters.Auth2AccessToken + "=" + this.AuthToken);
                var serializer = new JavaScriptSerializer();

                var share = new
                {
                    comment = comment,
                    visibility = new { code = "connections-only" }
                };

                var serializedResult = serializer.Serialize(share);
                StringContent content = new StringContent(serializedResult);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PostAsync(uri, content).Result;
                string st = response.Content.ReadAsStringAsync().Result;

                return st;
            }
        }
    }
}
