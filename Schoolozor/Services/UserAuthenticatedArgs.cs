﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolozor.Services
{
    public class UserAuthenticatedArgs : EventArgs
    {
        public UserAuthenticatedArgs(string s) { UserId = s; }
        public String UserId { get; } // readonly
    }
}
