using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(IdentityUser user, ulong companyId);

        DateTime GetExpirationDate();
    }
}
