using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio
{
    public class Users : Auth
    {
        public class NotAuthorised : Exception
        {
            public NotAuthorised(string message) : base(message) {}
        }

        public Users(Auth auth) : base(auth)
        {
        }

        public Responses.Users.List List()
        {
            try
            {
                var rs = this.client.OrganizationService.GetMembers(this.organisation_id);
                return new Responses.Users.List(rs);
            }
            catch (PodioAPI.Exceptions.PodioException ex)
            {
                throw new NotAuthorised(ex.Error.Error + ": " + ex.Error.ErrorDescription);
            }
        }
    }
}
