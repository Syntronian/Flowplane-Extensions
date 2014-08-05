using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;

namespace Extensions.Facebook
{
    public class Profile
    {
        private FacebookClient fbc { get; set; }
        private string appId { get; set; }
        private dynamic fbProfile { get; set; }

        public Profile(string appToken, string appId)
        {
            this.fbc = new FacebookClient(appToken);
            this.appId = appId;
            this.fbProfile = this.fbc.Get("/me");
        }

        public string Id
        {
            get { return this.fbProfile.id; }
        }

        public string Name
        {
            get { return (this.fbProfile.name ?? "").Replace(" ", ""); }
        }

        public string Email
        {
            get { return this.fbProfile.email; }
        }
    }
}
