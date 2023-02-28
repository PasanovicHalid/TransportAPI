using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TransportLibrary.Authentication.Model;
using TransportLibrary.Authentication.Services;
using TransportLibrary.Authentication.Services.Interfaces;
using TransportLibrary.Initializer;
using TransportLibrary.Settings;

namespace TransportLibrary
{
    public static class ProgramExpansions
    {
        public static void SetupDependencies(this IServiceCollection services)
        {
            services.AddTransient<IJwtGeneratorService, JwtGeneratorService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<DbInitializer>();
        }
    }
}