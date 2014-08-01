using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace Extensions.Paymo
{
    public class TaskLists : Auth
    {
        public TaskLists(Auth auth)
            : base(auth)
        {
        }

        public Responses.TaskLists.List List(string project_id)
        {
            // build URL params
            var args = new StringBuilder();
            args.Append("?api_key="); args.Append(System.Web.HttpUtility.UrlEncode(this.API_Key));
            args.Append("&auth_token="); args.Append(System.Web.HttpUtility.UrlEncode(this.Auth_Token));
            args.Append("&project_id="); args.Append(System.Web.HttpUtility.UrlEncode(project_id));

            // send
            var rs = this.HttpClient.GetStringAsync(baseURL + "paymo.tasklists.findByProject" + args.ToString()).Result;

            // parse, return response
            return new Responses.TaskLists.List(rs);
        }
    }
}
