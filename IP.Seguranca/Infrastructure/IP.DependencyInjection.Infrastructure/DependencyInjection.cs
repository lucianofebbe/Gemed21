using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;
using IP.Application.Handlers.Authentication;
using IP.Cryptography.AesCryptography;
using IP.Cryptography.PBKDF2Cryptography;
using IP.Cryptography.TokenCryptography;
using IP.Domain;
using IP.Logs;
using IP.Repository.Infrastructure.Repositories.SegEmpresa;
using IP.Repository.Infrastructure.Repositories.SegUsuario;
using IP.Repository.Infrastructure.Unit;
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
            services.AddTransient<IUnitOfWork<SegEmpresa>, UnitOfWork<SegEmpresa>>();
            //services.AddTransient<IUnitOfWork<SegUsuario>, UnitOfWork<SegUsuario>>();

            services.AddTransient<ISegEmpresaRepository<SegEmpresa>, SegEmpresaRepository>();
            //services.AddTransient<ISegUsuarioRepository<SegUsuario>, SegUsuarioRepository>();

            //Mapper
            //services.AddTransient<IGenerateMapper<BaseDomain, BaseRequest, BaseResponse>, GenerateMapper<BaseDomain, BaseRequest, BaseResponse>>();
            //services.AddTransient<IUsuarioMapper<BaseDomain, BaseRequest, BaseResponse>, IUsuarioMapper<BaseDomain, BaseRequest, BaseResponse>>();
            //services.AddTransient<IUsuarioAcessoMapper<BaseDomain, BaseRequest, BaseResponse>, IUsuarioAcessoMapper<BaseDomain, BaseRequest, BaseResponse>>();

            //services.AddTransient<IUsuarioMapper<Domain.Usuario, CreateUsuarioRequest, CreateUsuarioResponse>, UsuarioMapper<Domain.Usuario, CreateUsuarioRequest, CreateUsuarioResponse>>();
            //services.AddTransient<IUsuarioMapper<Domain.Usuario, GetByCPFRequest, GetByCPFResponse>, UsuarioMapper<Domain.Usuario, GetByCPFRequest, GetByCPFResponse>>();
            //services.AddTransient<IUsuarioMapper<Domain.Usuario, SendInviteRequest, SendInviteResponse>, UsuarioMapper<Domain.Usuario, SendInviteRequest, SendInviteResponse>>();
            //services.AddTransient<IUsuarioMapper<Domain.Usuario, AuthenticationRequest, AuthenticationResponse>, UsuarioMapper<Domain.Usuario, AuthenticationRequest, AuthenticationResponse>>();
            //services.AddTransient<IUsuarioMapper<Domain.Usuario, VincularClienteRequest, VincularClienteResponse>, UsuarioMapper<Domain.Usuario, VincularClienteRequest, VincularClienteResponse>>();
            //services.AddTransient<IUsuarioAcessoMapper<UsuarioAcesso, VincularClienteRequestService, VincularClienteResponseService>, UsuarioAcessoMapper<Domain.UsuarioAcesso, VincularClienteRequestService, VincularClienteResponseService>>();

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
            //services.AddTransient<IDefault<VincularClienteRequestService, VincularClienteResponseService>, Default<VincularClienteRequestService, VincularClienteResponseService>>();
            //services.AddTransient<IRequestReply<VincularClienteRequestService, VincularClienteResponseService>, RequestReply<VincularClienteRequestService, VincularClienteResponseService>>();

            //Services
            //services.AddTransient<IVincularClienteService<VincularClienteRequestService, VincularClienteResponseService>, VincularClienteService<VincularClienteRequestService, VincularClienteResponseService>>();

            //Handlers
            services.AddTransient<IRequestHandler<AuthenticationRequest, AuthenticationResponse>, AuthenticationHandler>();

            return services;
        }
}
}
