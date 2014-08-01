using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;


namespace Extensions.Paymo
{
    public class Users : Auth
    {
        public Users(Auth auth) 
            : base(auth) 
        {
        }

        public Responses.Users.List List()
        {
            // build URL params
            var args = new StringBuilder();
            args.Append("?api_key="); args.Append(System.Web.HttpUtility.UrlEncode(this.API_Key));
            args.Append("&auth_token="); args.Append(System.Web.HttpUtility.UrlEncode(this.Auth_Token));

            // send
            var rs = this.HttpClient.GetStringAsync(baseURL + "paymo.users.getList" + args.ToString()).Result;

            // parse, return response
            return new Responses.Users.List(rs);
        }
    }
}
