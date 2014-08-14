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

    public class Tokens : Auth
    {
        public const string requestTokenURL = "https://www.linkedin.com/uas/oauth2/authorization";
        public const string accessTokenURL = "https://www.linkedin.com/uas/oauth2/accessToken";
        
        public Tokens(Auth auth)
            : base(auth)
        { }

        public string GetRequestURL()
        {
            using (HttpClient client = new HttpClient())
            {
                StringBuilder QueryStringRequest = new StringBuilder();
                QueryStringRequest.AppendFormat("{0}", requestTokenURL);
                QueryStringRequest.AppendFormat("?{0}={1}", Parameters.RespType, "code");
                QueryStringRequest.AppendFormat("&{0}={1}", Parameters.ClientId, this.ConsumerKey);
                QueryStringRequest.AppendFormat("&{0}={1}", Parameters.Scope, this.LinkedInScope);
                QueryStringRequest.AppendFormat("&{0}={1}", Parameters.State, this.GetState());
                QueryStringRequest.AppendFormat("&{0}={1}", Parameters.CallbackUrl, this.CallbackUrl);
                string reqURL = QueryStringRequest.ToString();
                return reqURL;
            }
        }


        public string GetAccessToken(string authorizationCode)
        {
            using (HttpClient client = new HttpClient())
            {
                StringBuilder QueryStringRequest = new StringBuilder();
                QueryStringRequest.AppendFormat("{0}", accessTokenURL);
                QueryStringRequest.AppendFormat("?{0}={1}", Parameters.GrantType, "authorization_code");
                QueryStringRequest.AppendFormat("&{0}={1}", Parameters.Code, authorizationCode);
                QueryStringRequest.AppendFormat("&{0}={1}", Parameters.CallbackUrl, this.CallbackUrl);
                QueryStringRequest.AppendFormat("&{0}={1}", Parameters.ClientId, this.ConsumerKey);
                QueryStringRequest.AppendFormat("&{0}={1}", Parameters.ClientSecret, this.ConsumerSecret);

                StringContent content = new StringContent(String.Empty);
                content.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

                var response = client.PostAsync(QueryStringRequest.ToString(), content).Result;
                string res = response.Content.ReadAsStringAsync().Result;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var at = serializer.Deserialize<ResponseObject>(res);
                return at.access_token;
            }
        }

       
    }
}
