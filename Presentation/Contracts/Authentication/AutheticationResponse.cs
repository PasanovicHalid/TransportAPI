﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Authentication
{
    public class AutheticationResponse
    {
        public string Token { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
