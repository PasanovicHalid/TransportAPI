using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Infrastructure.Authentication;
using Infrastructure.Common.Persistence;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            SetupPersistance(services, configuration);

            SetupAuthentification(services, configuration);

            return services;
        }

        private static void SetupPersistance(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransportDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TransportDB")));

            services.AddScoped<DbInitializer>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void SetupAuthentification(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
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
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    ValidAudience = configuration["Jwt:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public static async Task InitializeDb(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<DbInitializer>().Initialize();
        }
    }


}