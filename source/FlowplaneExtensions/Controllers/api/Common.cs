using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Formatting;
namespace FlowplaneExtensions.Controllers.api
{
    public class Common
    {
        internal static string GetValue(FormDataCollection data, string key)
        {
            return data.FirstOrDefault(a => a.Key == key).Value;
        }
    }
}