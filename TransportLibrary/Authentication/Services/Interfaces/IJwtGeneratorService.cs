using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Authentication.Model;

namespace TransportLibrary.Authentication.Services.Interfaces
{
    public interface IJwtGeneratorService
    {
        string GenerateToken(ApplicationUser user);
    }
}
