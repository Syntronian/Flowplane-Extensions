using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowplaneExtensions.Tests.ExtensionTests
{
    public class Common
    {
        public static void Display(dynamic res)
        {
            foreach (var i in res.items)
            {
                Console.WriteLine("id: " + i.id);
                Console.WriteLine("name: " + i.name);
            }
        }
    }
}
