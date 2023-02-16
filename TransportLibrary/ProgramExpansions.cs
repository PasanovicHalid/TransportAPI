using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TransportLibrary.Authentication;
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

        public static void SetupDBs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransportDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TransportDB")));
        }

        public static void SetupSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        }

        public static void SetupAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                //options.SignIn.RequireConfirmedEmail = true;
                //options.SignIn.RequireConfirmedAccount = true;
                //options.Lockout.MaxFailedAccessAttempts = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            })
                .AddEntityFrameworkStores<TransportDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidAudience = configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public static void InitializeDB(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<DbInitializer>().Initialize();
            }
        }

    }
}