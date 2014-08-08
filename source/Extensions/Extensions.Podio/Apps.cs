using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio
{
    public class Apps : Auth
    {
        public Apps(Auth auth) : base(auth)
        {
        }

        public Responses.Apps.List List(int spaceId)
        {
            try
            {
                var rs = this.client.ApplicationService.GetAppsBySpace(spaceId);
                return new Responses.Apps.List(rs);
            }
            catch (PodioAPI.Exceptions.PodioException ex)
            {
                throw new Exception(ex.Error.Error + ": " + ex.Error.ErrorDescription);
            }
        }
    }
}
