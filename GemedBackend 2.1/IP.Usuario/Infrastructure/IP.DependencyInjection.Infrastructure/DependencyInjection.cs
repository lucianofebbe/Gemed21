using IP.Application.Comands.Requests.Menu;
using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Menu;
using IP.Application.Comands.Responses.Usuario;
using IP.Application.Handlers;
using IP.Application.Handlers.Menu;
using IP.Application.Handlers.Usuario;
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
            services.AddTransient<IRequestHandler<UsuarioCreateRequest, UsuarioCreateResponse>, CreateHandler>();
            services.AddTransient<IRequestHandler<UsuarioGetByCPFRequest, UsuarioGetByCPFResponse>, GetByCPFHandler>();
            services.AddTransient<IRequestHandler<UsuarioGetByIdRequest, UsuarioGetByIdResponse>, GetByIdHandler>();
            services.AddTransient<IRequestHandler<UsuarioSendInviteRequest, UsuarioSendInviteResponse>, SendInviteHandler>();
            services.AddTransient<IRequestHandler<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>, VincularClinicaHandler>();
            services.AddTransient<IRequestHandler<AuthenticationRequest, AuthenticationResponse>, AuthenticationHandler>();
            services.AddTransient<IRequestHandler<MenuRequest, MenuResponse>, MenuHandler>();

            return services;
        }
    }
}
