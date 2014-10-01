using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowplaneExtensions.Models.api.Flow
{
    public class Detail
    {
        public string extensionCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public List<FpxtParam> parameters { get; set; }

        public string GetParamValue(string key)
        {
            var pm = this.parameters.FirstOrDefault(p => p.key == key);
            return pm == null ? "" : pm.value;
        }
    }
}