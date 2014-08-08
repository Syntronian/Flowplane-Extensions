using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions.Podio.Responses.Tasks;
using Newtonsoft.Json;
using PodioAPI;
using PodioAPI.Models;
using PodioAPI.Utils.Authentication;

namespace Extensions.Podio
{
    public class Auth
    {
        internal PodioAPI.Podio client { get; set; }
        internal PodioAPI.Utils.Authentication.PodioOAuth auth { get; set; }
        public int organisation_id { get; set; }

        public Auth(Auth auth)
        {
            this.client = auth.client;
            this.auth = auth.auth;
            this.organisation_id = auth.organisation_id;
        }

        public Auth(string client_id, string client_secret)
        {
            this.client = new PodioAPI.Podio(client_id, client_secret);
        }
        
        public Auth(string client_id, string client_secret, string access_token, int? organisation_id = null)
        {
            this.client = new PodioAPI.Podio(client_id, client_secret);
            this.auth = new PodioOAuth()
            {
                AccessToken = access_token,
                //ExpiresIn = "",
                //RefreshToken = "",
                //TokenType = "",
                //Ref = new Ref()
                //{
                //    Id = refId,
                //    Type = refType
                //}
            };
            this.client.OAuth = this.auth;

            if (organisation_id.HasValue)
                this.organisation_id = (int)organisation_id;
        }

        public string AccessToken
        {
            get
            {
                return this.auth.AccessToken;
            }
        }

        public string GetLoginUrl(string redirect_uri)
        {
            return this.client.GetAuthorizeUrl(redirect_uri);
        }

        public string GetAccessToken(string code, string redirect_uri)
        {
            this.auth = this.client.AuthenticateWithAuthorizationCode(code, redirect_uri);
            return this.auth.AccessToken;
        }
    }
}
