using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace Extensions.Asana
{
    public class Tasks : Auth
    {
        public const string baseURL = "https://app.asana.com/api/1.0/tasks/";

        public Tasks(Auth auth) : base(auth) 
        {
        }

        public Responses.Tasks.Get Get(string id)
        {
            // post
            var rs = this.HttpClient.GetStringAsync(baseURL + id).Result;

            // parse, return response
            return new Responses.Tasks.Get(rs);
        }

        public Responses.Tasks.Create Create(string workspace_id, string project_id, 
                                             string name, string assignee_id, int? daysDue)
        {
            // set up post data
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("workspace", workspace_id));
            postData.Add(new KeyValuePair<string, string>("projects[0]", project_id));
            postData.Add(new KeyValuePair<string, string>("name", name));
            postData.Add(new KeyValuePair<string, string>("assignee", assignee_id));
            if (daysDue.HasValue)
            {
                postData.Add(new KeyValuePair<string, string>("due_on", DateTime.UtcNow.AddDays((int) daysDue).ToString("yyyy-MM-dd")));
            }
            HttpContent content = new FormUrlEncodedContent(postData);

            // post
            var rs = this.HttpClient.PostAsync(baseURL, content).Result;
            if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

            // parse, return response
            return new Responses.Tasks.Create(rs.Content.ReadAsStringAsync().Result);
        }

        public void Update(string id, bool? completed)
        {
            // set up post data
            var postData = new List<KeyValuePair<string, string>>();
            if (completed.HasValue)
                postData.Add(new KeyValuePair<string, string>("completed", Convert.ToBoolean(completed) ? "true" : "false"));
            HttpContent content = new FormUrlEncodedContent(postData);

            // post
            var rs = this.HttpClient.PutAsync(baseURL + id, content).Result;
            if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

            // parse, check response
            var res = rs.Content.ReadAsStringAsync().Result;
        }
    }
}
