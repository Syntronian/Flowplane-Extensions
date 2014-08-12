using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Extensions.Wrike.Responses.Folders;
using System.Collections.Specialized;
namespace Extensions.Wrike
{
    public class Folders : Auth
    {
        public const string baseURL = "https://www.wrike.com/api/json/v2/wrike.folder.tree";

        public Folders(Auth auth) : base(auth)
        {
        }

        public Get Get()
        {
            using (var client = new HttpClient())
            {
                var reqURL = this.GenerateUrl(baseURL, new NameValueCollection
                {
                    {Auth.Parameters.FolderIncludeChilds, "true"},
                    {Auth.Parameters.FolderParentId, "0"}
                });

                var content = new StringContent(reqURL);
                content.Headers.ContentType.MediaType = "application/json";
                var response = client.PostAsync(reqURL, content).Result;

                return new Get(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
