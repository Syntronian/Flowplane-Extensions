using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio
{
    public class Workspaces : Auth
    {
        public Workspaces(Auth auth) : base(auth)
        {
        }

        public Responses.Workspaces.List List()
        {
            try
            {
                var rs = this.client.OrganizationService.GetSpacesOnOrganization(this.organisation_id);
                return new Responses.Workspaces.List(rs);
            }
            catch (PodioAPI.Exceptions.PodioException ex)
            {
                throw new Exception(ex.Error.Error + ": " + ex.Error.ErrorDescription);
            }
        }
    }
}
