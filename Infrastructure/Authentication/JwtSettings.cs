using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class JwtSettings
    {
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
        public int ExpirationHours { get; set; }
        public string? SecretKey { get; set; }
    }
}
