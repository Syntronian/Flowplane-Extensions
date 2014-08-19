using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;

namespace Extensions.Facebook
{
    public class Status
    {
        private FacebookClient fbc { get; set; }

        public Status(string appToken)
        {
            this.fbc = new FacebookClient(appToken);
        }

        public void UpdateStatus(
            string message,
            string linkUrl = null,
            string pictureUrl = null,
            string name = null,
            string caption = null,
            string description = null,
            string action_name = null,
            string action_linkUrl = null)
        {
            dynamic parameters = new ExpandoObject();
            parameters.message = message;
            if (!string.IsNullOrEmpty(linkUrl))     parameters.link = linkUrl;
            if (!string.IsNullOrEmpty(pictureUrl))  parameters.picture = pictureUrl;
            if (!string.IsNullOrEmpty(name))        parameters.name = name;
            if (!string.IsNullOrEmpty(caption))     parameters.caption = caption;
            if (!string.IsNullOrEmpty(description)) parameters.description = description;
            if (!string.IsNullOrEmpty(action_name))
                parameters.actions = new
                {
                    name = action_name,
                    link = action_linkUrl,
                };
            parameters.privacy = new
            {
                value = "ALL_FRIENDS",
            };
            //parameters.targeting = new
            //{
            //    countries = "US",
            //    regions = "6,53",
            //    locales = "6",
            //};

            dynamic rs = this.fbc.Post("me/feed", parameters);
        }
    }
}
