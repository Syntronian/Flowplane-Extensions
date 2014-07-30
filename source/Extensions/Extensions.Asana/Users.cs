using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace Extensions.Asana
{
    public class Users : Auth
    {
        public const string baseURL = "https://app.asana.com/api/1.0/users/";

        public Users(Auth auth) : base(auth) 
        {
        }

        public Responses.Users.List List()
        {
            // post
            var rs = this.HttpClient.GetStringAsync(baseURL).Result;

            // parse, return response
            return new Responses.Users.List(rs);
        }

        public Responses.Users.Get Get(string id)
        {
            // post
            var rs = this.HttpClient.GetStringAsync(baseURL + id).Result;

            // parse, return response
            return new Responses.Users.Get(rs);
        }
    }
}
