using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace Extensions.Paymo
{
    public class Tasks : Auth
    {
        public Tasks(Auth auth) 
            : base(auth) 
        {
        }

        public Responses.Tasks.Get Get(string id)
        {
            // build URL params
            var args = new StringBuilder();
            args.Append("?api_key="); args.Append(System.Web.HttpUtility.UrlEncode(this.API_Key));
            args.Append("&auth_token="); args.Append(System.Web.HttpUtility.UrlEncode(this.Auth_Token));
            args.Append("&task_id="); args.Append(System.Web.HttpUtility.UrlEncode(id));

            // send
            var rs = this.HttpClient.GetStringAsync(baseURL + "paymo.tasks.getInfo" + args.ToString()).Result;

            // parse, return response
            return new Responses.Tasks.Get(rs);
        }

        public Responses.Tasks.Add Add(string project_id,
                                       string tasklist_id,
                                       string name, string assignee_id, int? daysDue)
        {
            // set up post data
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("api_key", this.API_Key));
            postData.Add(new KeyValuePair<string, string>("auth_token", this.Auth_Token));
            postData.Add(new KeyValuePair<string, string>("name", name));
            postData.Add(new KeyValuePair<string, string>("project_id", project_id));
            if (!string.IsNullOrEmpty(tasklist_id))
                postData.Add(new KeyValuePair<string, string>("tasklist_id", tasklist_id));
            if (daysDue.HasValue)
            {
                postData.Add(new KeyValuePair<string, string>("due_date", DateTime.UtcNow.AddDays((int)daysDue).ToString("yyyy-MM-dd")));
            }
            postData.Add(new KeyValuePair<string, string>("user_id", assignee_id));
            HttpContent content = new FormUrlEncodedContent(postData);

            // send
            var rs = this.HttpClient.PostAsync(baseURL + "paymo.tasks.add", content).Result;
            if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

            // parse, return response
            return new Responses.Tasks.Add(rs.Content.ReadAsStringAsync().Result);
        }

        public Responses.Tasks.Update Update(string task_id, bool? completed)
        {
            // set up post data
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("api_key", this.API_Key));
            postData.Add(new KeyValuePair<string, string>("auth_token", this.Auth_Token));
            postData.Add(new KeyValuePair<string, string>("task_id", task_id));
            if (completed.HasValue)
                postData.Add(new KeyValuePair<string, string>("complete", Convert.ToBoolean(completed) ? "1" : "0"));
            HttpContent content = new FormUrlEncodedContent(postData);

            // send
            var rs = this.HttpClient.PostAsync(baseURL + "paymo.tasks.update", content).Result;
            if (!rs.IsSuccessStatusCode) throw new Exception(rs.ReasonPhrase);

            // parse, return response
            return new Responses.Tasks.Update(rs.Content.ReadAsStringAsync().Result);
        }
    }
}
