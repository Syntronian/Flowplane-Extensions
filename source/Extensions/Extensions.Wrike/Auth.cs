using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using ExtensionsCore;

namespace Extensions.Wrike
{
    public class Auth : IAuth
    {
        public static class Parameters
        {
            public const string OAuth_Callback = "oauth_callback";
            public const string OAuth_Consumer_Key = "oauth_consumer_key";
            public const string OAuth_Nonce = "oauth_nonce";
            public const string OAuth_Session_Handle = "oauth_session_handle";
            public const string OAuth_Signature = "oauth_signature";
            public const string OAuth_Signature_Method = "oauth_signature_method";
            public const string OAuth_Timestamp = "oauth_timestamp";
            public const string OAuth_Token = "oauth_token";
            public const string OAuth_Token_Secret = "oauth_token_secret";
            public const string OAuth_Version = "oauth_version";

            public const string FolderIncludeChilds = "includeChilds";
            public const string FolderParentId = "parentId";
            public const string TaskID = "id";
            public const string TaskStatus = "status";
            public const string TaskTitle = "title";
            public const string TaskDescription = "description";
            public const string TaskStartDate = "startDate";
            public const string TaskDueDate = "dueDate";
            public const string TaskAssignee = "responsibleUsers";
            public const string TaskSharedTo = "sharedUsers";
            public const string TaskParentIDs = "parents";
        }

        private const string version = "1.0";
        private const string signatureMethod = "PLAINTEXT";

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AuthToken { get; set; }
        public string AuthTokenSecret { get; set; }
        public string callbackUrl { get; set; }

        public Auth(Auth auth)
        {
            this.ConsumerKey = auth.ConsumerKey;
            this.ConsumerSecret = auth.ConsumerSecret;
            this.AuthToken = auth.AuthToken;
            this.AuthTokenSecret = auth.AuthTokenSecret;
            this.callbackUrl = auth.callbackUrl;
        }

        public Auth(string consumer_key, string consumer_secret, string oauth_token, string oauth_token_secrect, string callback_url)
        {
            this.ConsumerKey = consumer_key;
            this.ConsumerSecret = consumer_secret;
            this.AuthToken = oauth_token;
            this.AuthTokenSecret = oauth_token_secrect;
            this.callbackUrl = callback_url;
        }

        public string GenerateUrl(string apiMethod, NameValueCollection addtionalParams = null)
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
            var nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString())); ;

            var rq = new StringBuilder();
            rq.AppendFormat("{0}", apiMethod);
            rq.AppendFormat("?{0}={1}", Parameters.OAuth_Version, version);
            rq.AppendFormat("&{0}={1}", Parameters.OAuth_Signature_Method, signatureMethod);
            rq.AppendFormat("&{0}={1}", Parameters.OAuth_Consumer_Key, this.ConsumerKey);
            rq.AppendFormat("&{0}={1}", Parameters.OAuth_Signature, this.ConsumerSecret + "%26" + this.AuthTokenSecret);
            rq.AppendFormat("&{0}={1}", Parameters.OAuth_Nonce, nonce);
            rq.AppendFormat("&{0}={1}", Parameters.OAuth_Timestamp, timestamp);

            if (!string.IsNullOrEmpty(this.AuthToken))
                rq.AppendFormat("&{0}={1}", Parameters.OAuth_Token, this.AuthToken);

            if (!string.IsNullOrEmpty(this.callbackUrl))
                rq.AppendFormat("&{0}={1}", Parameters.OAuth_Callback, this.callbackUrl);

            if (addtionalParams != null)
            {
                foreach (string p in addtionalParams)
                {
                    rq.AppendFormat("&{0}={1}", p, addtionalParams.GetValues(p)[0]);
                }
            }

            return rq.ToString();
        }
    }
}
