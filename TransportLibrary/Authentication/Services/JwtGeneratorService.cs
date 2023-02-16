using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Authentication.Services.Interfaces;
using TransportLibrary.Settings;

namespace TransportLibrary.Authentication.Services
{
    public class JwtGeneratorService : IJwtGeneratorService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtGeneratorService(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
        }
        public string GenerateToken(ApplicationUser user)
        {
            SecurityTokenDescriptor tokenDescriptor = SetupTokenDescriptor(GetUserClaimsAsync(user).GetAwaiter().GetResult());

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor SetupTokenDescriptor(List<Claim> userClaims)
        {
            byte[] secretKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
                Issuer = _jwtSettings.ValidIssuer,
                Audience = _jwtSettings.ValidAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512Signature)
            };
            return tokenDescriptor;
        }

        private async Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            List<Claim> userClaims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            foreach (string role in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            return userClaims;
        }
    }
}
