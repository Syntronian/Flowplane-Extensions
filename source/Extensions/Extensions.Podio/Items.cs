using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Podio
{
    public class Items : Auth
    {
        public Items(Auth auth) : base(auth)
        {
        }

        public Responses.Items.List List(int appId)
        {
            try
            {
                var rs = this.client.ItemService.FilterItems(appId);
                return new Responses.Items.List(rs);
            }
            catch (PodioAPI.Exceptions.PodioException ex)
            {
                throw new Exception(ex.Error.Error + ": " + ex.Error.ErrorDescription);
            }
        }
    }
}
