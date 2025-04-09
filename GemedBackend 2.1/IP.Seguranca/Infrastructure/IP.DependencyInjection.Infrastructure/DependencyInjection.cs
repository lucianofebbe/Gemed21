using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Requests.Authorization;
using IP.Application.Comands.Responses.Authentication;
using IP.Application.Comands.Responses.Authorization;
using IP.Application.Handlers.Authentication;
using IP.Application.Handlers.Authorization;
using IP.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.IP.DependencyInjection.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            //Services
            services.AddTransient<IServicesFactory, ServicesFactory>();

            //Handlers
            services.AddTransient<IRequestHandler<AuthenticationRequest, AuthenticationResponse>, AuthenticationHandler>();
            services.AddTransient<IRequestHandler<AuthorizationRequest, AuthorizationResponse>, AuthorizationHandler>();

            return services;
        }
    }
}
