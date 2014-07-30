using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace Extensions.Asana
{
    public class Workspaces : Auth
    {
        public const string baseURL = "https://app.asana.com/api/1.0/workspaces/";

        public Workspaces(Auth auth)
            : base(auth)
        {
        }

        public Responses.Workspaces.List List()
        {
            // post
            var rs = this.HttpClient.GetStringAsync(baseURL).Result;

            // parse, return response
            return new Responses.Workspaces.List(rs);
        }
    }
}
