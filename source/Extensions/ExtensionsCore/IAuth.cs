using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IAuth
    {
        string ConsumerKey { get; }
        string ConsumerSecret { get; }
        string AuthToken { get; }
        string AuthTokenSecret { get; }
    }
}
