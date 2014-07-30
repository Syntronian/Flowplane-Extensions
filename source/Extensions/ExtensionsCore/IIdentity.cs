﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionsCore
{
    public interface IIdentity
    {
        string Code { get; }
        string Name { get; }
        Definitions.ProcessObjectTypes Type { get; }
    }
}