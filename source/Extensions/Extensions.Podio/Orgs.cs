using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio
{
    public class Orgs : Auth
    {
        public Orgs(Auth auth) : base(auth)
        {
        }

        public Responses.Orgs.List List()
        {
            try
            {
                var rs = this.client.OrganizationService.GetOrganizations();
                return new Responses.Orgs.List(rs);
            }
            catch (PodioAPI.Exceptions.PodioException ex)
            {
                throw new Exception(ex.Error.Error + ": " + ex.Error.ErrorDescription);
            }
        }
    }
}
