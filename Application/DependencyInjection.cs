using Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
                .AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Scoped);
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, ServiceLifetime.Singleton);

            return services;
        }
    }
}