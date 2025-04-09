using IP.Application.Comands.Requests;
using IP.Application.Comands.Responses;
using IP.Application.Handlers;
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
            
            services.AddTransient<IServicesFactory, ServicesFactory>();

            //Handlers
            services.AddTransient<IRequestHandler<Request, Response>, GatewayHandler>();


            return services;
        }
    }
}
