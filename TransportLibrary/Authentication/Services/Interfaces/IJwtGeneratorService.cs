using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportLibrary.Authentication.Services.Interfaces
{
    public interface IJwtGeneratorService
    {
        string GenerateToken(ApplicationUser user);
    }
}
