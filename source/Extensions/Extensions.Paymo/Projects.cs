using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace Extensions.Paymo
{
    public class Projects : Auth
    {
        public Projects(Auth auth) 
            : base(auth) 
        {
        }

        public Responses.Projects.List List()
        {
            // build URL params
            var args = new StringBuilder();
            args.Append("?api_key="); args.Append(System.Web.HttpUtility.UrlEncode(this.API_Key));
            args.Append("&auth_token="); args.Append(System.Web.HttpUtility.UrlEncode(this.Auth_Token));

            // send
            var rs = this.HttpClient.GetStringAsync(baseURL + "paymo.projects.getList" + args.ToString()).Result;

            // parse, return response
            return new Responses.Projects.List(rs);
        }

        public Responses.Projects.Get Get(string id)
        {
            // build URL params
            var args = new StringBuilder();
            args.Append("?api_key="); args.Append(System.Web.HttpUtility.UrlEncode(this.API_Key));
            args.Append("&auth_token="); args.Append(System.Web.HttpUtility.UrlEncode(this.Auth_Token));
            args.Append("&project_id="); args.Append(System.Web.HttpUtility.UrlEncode(id));

            // send
            var rs = this.HttpClient.GetStringAsync(baseURL + "paymo.projects.getInfo" + args.ToString()).Result;

            // parse, return response
            return new Responses.Projects.Get(rs);
        }

        public Responses.Projects.Add Add(string client_id, string name)
        {
            // set up post data
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("api_key", this.API_Key));
            postData.Add(new KeyValuePair<string, string>("auth_token", this.Auth_Token));
            postData.Add(new KeyValuePair<string, string>("name", name));
            postData.Add(new KeyValuePair<string, string>("client_id", client_id));
            HttpContent content = new FormUrlEncodedContent(postData);

            // send
            var rs = this.HttpClient.PostAsync(baseURL + "paymo.projects.add", content).Result;
            if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

            // parse, return response
            return new Responses.Projects.Add(rs.Content.ReadAsStringAsync().Result);
        }

        public Responses.Projects.GetTasks GetTasks(string project_ID)
        {
            // build URL params
            var args = new StringBuilder();
            args.Append("?api_key="); args.Append(System.Web.HttpUtility.UrlEncode(this.API_Key));
            args.Append("&auth_token="); args.Append(System.Web.HttpUtility.UrlEncode(this.Auth_Token));
            args.Append("&project_id="); args.Append(System.Web.HttpUtility.UrlEncode(project_ID));

            // send
            var rs = this.HttpClient.GetStringAsync(baseURL + "paymo.tasks.findByProject" + args.ToString()).Result;

            // parse, return response
            return new Responses.Projects.GetTasks(rs);
        }
    }
}
