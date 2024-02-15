using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Requests.Services;
using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Authentication;
using IP.Application.Comands.Responses.Services;
using IP.Application.Comands.Responses.Usuario;
using IP.Application.Handlers;
using IP.Application.Handlers.Usuario;
using IP.BaseDomains;
using IP.Cryptography.AesCryptography;
using IP.Cryptography.PBKDF2Cryptography;
using IP.Cryptography.TokenCryptography;
using IP.Domain;
using IP.Logs;
using IP.Mapper;
using IP.Mapper.Infrastructure.Usuario;
using IP.Mapper.Infrastructure.UsuarioAcesso;
using IP.RabbitMQ.Default;
using IP.RabbitMQ.RequestReply;
using IP.Repository.Infrastructure.Unit;
using IP.Repository.Infrastructure.UsuarioAcessos;
using IP.Repository.Infrastructure.Usuarios;
using IP.Services.VincularCliente;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static IP.Mails.ISendMails;
using static IP.Mails.SendMails;

namespace Infrastructure.IP.DependencyInjection.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //    //services.AddDbContext<Context>(options =>
            //    //    options.UseSqlServer(
            //    //        configuration.GetConnectionString("IpAutenticacao"),
            //    //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //Repo
            services.AddTransient<IUnitOfWork<Usuario>, UnitOfWork<Usuario>>();
            services.AddTransient<IUnitOfWork<UsuarioAcesso>, UnitOfWork<UsuarioAcesso>>();

            services.AddTransient<IUsuarioRepository<Usuario>, UsuariosRepository>();
            services.AddTransient<IUsuarioAcessoRepository<UsuarioAcesso>, UsuarioAcessoRepository>();

            //Mapper
            services.AddTransient<IGenerateMapper<BaseDomain, BaseRequest, BaseResponse>, GenerateMapper<BaseDomain, BaseRequest, BaseResponse>>();
            services.AddTransient<IUsuarioMapper<BaseDomain, BaseRequest, BaseResponse>, IUsuarioMapper<BaseDomain, BaseRequest, BaseResponse>>();
            services.AddTransient<IUsuarioAcessoMapper<BaseDomain, BaseRequest, BaseResponse>, IUsuarioAcessoMapper<BaseDomain, BaseRequest, BaseResponse>>();

            services.AddTransient<IUsuarioMapper<Usuario, CreateUsuarioRequest, CreateUsuarioResponse>, UsuarioMapper<Usuario, CreateUsuarioRequest, CreateUsuarioResponse>>();
            services.AddTransient<IUsuarioMapper<Usuario, GetByCPFRequest, GetByCPFResponse>, UsuarioMapper<Usuario, GetByCPFRequest, GetByCPFResponse>>();
            services.AddTransient<IUsuarioMapper<Usuario, SendInviteRequest, SendInviteResponse>, UsuarioMapper<Usuario, SendInviteRequest, SendInviteResponse>>();
            services.AddTransient<IUsuarioMapper<Usuario, AuthenticationRequest, AuthenticationResponse>, UsuarioMapper<Usuario, AuthenticationRequest, AuthenticationResponse>>();
            services.AddTransient<IUsuarioMapper<Usuario, VincularClienteRequest, VincularClienteResponse>, UsuarioMapper<Usuario, VincularClienteRequest, VincularClienteResponse>>();
            services.AddTransient<IUsuarioAcessoMapper<UsuarioAcesso, VincularClienteRequestService, VincularClienteResponseService>, UsuarioAcessoMapper<UsuarioAcesso, VincularClienteRequestService, VincularClienteResponseService>>();

            //Cryptography
            services.AddTransient<IAesCryptography, AesCryptography>();
            services.AddTransient<IJwtConfiguration, JwtConfiguration>();
            services.AddTransient<ITokenCryptography, TokenCryptography>();
            services.AddTransient<IPBKDF2Cryptography, PBKDF2Cryptography>();

            //Logs
            services.AddTransient<IGenerateLogsConfig, GenerateLogsConfig>();
            services.AddTransient<IGenerateLogs, GenerateLogs>();

            //Mails
            services.AddTransient<IEmailSend, EmailSend>();

            //MQ
            services.AddTransient<IDefault<VincularClienteRequestService, VincularClienteResponseService>, Default<VincularClienteRequestService, VincularClienteResponseService>>();
            services.AddTransient<IRequestReply<VincularClienteRequestService, VincularClienteResponseService>, RequestReply<VincularClienteRequestService, VincularClienteResponseService>>();

            //Services
            services.AddTransient<IVincularClienteService<VincularClienteRequestService, VincularClienteResponseService>, VincularClienteService<VincularClienteRequestService, VincularClienteResponseService>>();

            //Handlers
            services.AddTransient<IRequestHandler<VincularClienteRequest, VincularClienteResponse>, VincularClienteHandlers>();
            services.AddTransient<IRequestHandler<CreateUsuarioRequest, CreateUsuarioResponse>, CreateUsuarioHandler>();
            services.AddTransient<IRequestHandler<GetByCPFRequest, GetByCPFResponse>, GetByCPFHandler>();
            services.AddTransient<IRequestHandler<SendInviteRequest, SendInviteResponse>, SendInviteHandler>();

            return services;
        }
}
}
