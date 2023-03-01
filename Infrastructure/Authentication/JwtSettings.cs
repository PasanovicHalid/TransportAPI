using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class JwtSettings
    {
        public string ValidIssuer { get; init; } = null!;
        public string ValidAudience { get; init; } = null!;
        public int ExpirationHours { get; init; } 
        public string SecretKey { get; init; } = null!;
    }
}
