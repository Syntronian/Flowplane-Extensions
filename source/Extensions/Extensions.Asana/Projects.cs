using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace Extensions.Asana
{
    public class Projects : Auth
    {
        public const string baseURL = "https://app.asana.com/api/1.0/projects/";

        public Projects(Auth auth) : base(auth)
        {
        }

        public Responses.Projects.List List(string workspace_ID = "", bool? archived = null)
        {
            var q = "";
            if (archived.HasValue) q = "?archived=" + archived.ToString();

            if (string.IsNullOrEmpty(workspace_ID))
            {
                // post
                var rs = this.HttpClient.GetStringAsync(baseURL + q).Result;

                // parse, return response
                return new Responses.Projects.List(rs);
            }
            else
            {
                // post
                var rs = this.HttpClient.GetStringAsync(Workspaces.baseURL + workspace_ID + "/projects" + q).Result;

                // parse, return response
                return new Responses.Projects.List(rs);
            }
        }

        public Responses.Projects.Get Get(string id)
        {
            // post
            var rs = this.HttpClient.GetStringAsync(baseURL + id).Result;

            // parse, return response
            return new Responses.Projects.Get(rs);
        }

        public Responses.Projects.Create Create(string workspace_ID, string name)
        {
            // set up post data
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add( new KeyValuePair<string, string>("name", name));
            postData.Add(new KeyValuePair<string, string>("workspace", workspace_ID));
            HttpContent content = new FormUrlEncodedContent(postData);

            // post
            var rs = this.HttpClient.PostAsync(baseURL, content).Result;
            if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

            // parse, return response
            return new Responses.Projects.Create(rs.Content.ReadAsStringAsync().Result);
        }

        public Responses.Projects.GetTasks GetTasks(string workspace_ID, string project_ID)
        {
            // post
            var rs = this.HttpClient.GetStringAsync(baseURL + project_ID + "/tasks").Result;

            // parse, return response
            return new Responses.Projects.GetTasks(rs, this.API_Key);
        }
    }
}
