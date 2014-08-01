using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;

namespace Extensions.Paymo
{
    public class Auth
    {
        private bool token_updated = false;
        private HttpClient httpClient = new HttpClient();

        public const string baseURL = "https://api.paymo.biz/service/";

        public Auth()
        {
        }

        public Auth(Auth auth)
        {
            this.API_Key = auth.API_Key;
            this.Username = auth.Username;
            this.Password = auth.Password;
            this.Auth_Token = auth.Auth_Token;
        }

        public string API_Key { get; set; }
        public string Auth_Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool TokenUpdated
        {
            get
            {
                return this.token_updated;
            }
        }
        public HttpClient HttpClient
        {
            get
            {
                return this.httpClient;
            }
        }

        // use this constructor mostly. will check the token first and use that if still valid, otherwise get a new one using api key
        public Auth(string apiKey, string username, string password, string auth_token)
        {
            this.API_Key = apiKey;
            this.Username = username;
            this.Password = password;
            this.Auth_Token = auth_token;

            // first check token
            try
            {
                this.CheckToken();
            }
            catch (Exception ex)
            {
                var dummy = ex.Message;

                // failed, log in again and get new token
                var auth = new Auth(apiKey, username, password);
                this.Auth_Token = auth.Auth_Token;
                this.token_updated = true;

                // check again
                this.CheckToken();
            }
        }

        public Auth(string apiKey, string username, string password)
        {
            this.API_Key = apiKey;
            this.Username = username;
            this.Password = password;

            // call login method, build URL params
            var args = new StringBuilder();
            args.Append("?api_key="); args.Append(System.Web.HttpUtility.UrlEncode(apiKey));
            args.Append("&username="); args.Append(System.Web.HttpUtility.UrlEncode(username));
            args.Append("&password="); args.Append(System.Web.HttpUtility.UrlEncode(password));

            // send
            var rs = this.httpClient.GetStringAsync(baseURL + "paymo.auth.login" + args.ToString()).Result;

            // parse response
            var xml = new System.Xml.XmlDocument();
            xml.LoadXml(rs);
            if (!xml.DocumentElement.HasAttributes) throw new Exception("Invalid response.");
            if (!xml.DocumentElement.Attributes[0].Value.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
                throw Auth.ParseError(rs);

            // get auth token
            this.Auth_Token = xml.DocumentElement.FirstChild.FirstChild.Value;
        }

        private void CheckToken()
        {
            // build URL params
            var args = new StringBuilder();
            args.Append("?api_key="); args.Append(System.Web.HttpUtility.UrlEncode(this.API_Key));
            args.Append("&auth_token="); args.Append(System.Web.HttpUtility.UrlEncode(this.Auth_Token));

            // send
            var rs = this.httpClient.GetStringAsync(baseURL + "paymo.auth.checkToken" + args.ToString()).Result;

            // parse response
            var xml = new System.Xml.XmlDocument();
            xml.LoadXml(rs);
            if (!xml.DocumentElement.HasAttributes) throw new Exception("Invalid response.");
            if (!xml.DocumentElement.Attributes[0].Value.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
                throw Auth.ParseError(rs);
        }
        
        public static Exception ParseError(string rs)
        {
            var xml = new System.Xml.XmlDocument();
            xml.LoadXml(rs);
            if (!xml.DocumentElement.HasAttributes) return new Exception("Invalid response.");
            if (!xml.DocumentElement.Attributes[0].Value.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
            {
                // attempt to parse error msg
                if (xml.DocumentElement.FirstChild.Attributes.Count < 2) return new Exception("Response not ok, unknown reason.");
                var xc = new Exception(xml.DocumentElement.FirstChild.Attributes[1].Value);
                xc.Data.Add("code", xml.DocumentElement.FirstChild.Attributes[0].Value);
                return xc;
            }
            return new Exception("Invalid response.");
        }
    }
}
