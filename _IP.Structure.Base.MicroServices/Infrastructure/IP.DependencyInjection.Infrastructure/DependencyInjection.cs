using IP.BaseDomains;
using IP.Cryptography.AesCryptography;
using IP.Cryptography.PBKDF2Cryptography;
using IP.Cryptography.TokenCryptography;
using IP.Domain;
using IP.Logs.GeneratorFactory;
using IP.Mails.MailsFactory;
using IP.MongoDb.MongoDbFactory;
using IP.RabbitMQ.RabbitMQFactory;
using IP.Repository.Infrastructure.RepositoryFactory;
using IP.Repository.Infrastructure.Unit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IP.DependencyInjection.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region IP.Infrastructure
            //Cryptography
            services.AddTransient<IAesCryptography, AesCryptography>();
            services.AddTransient<IJwtConfiguration, JwtConfiguration>();
            services.AddTransient<ITokenCryptography, TokenCryptography>();
            services.AddTransient<IPBKDF2Cryptography, PBKDF2Cryptography>();

            //MongoDataBase
            services.AddTransient(typeof(IMongoDbFactory<>), typeof(MongoDbFactory<>));

            //Mails
            services.AddTransient(typeof(IMailsFactory), typeof(MailsFactory));

            //Log
            services.AddTransient(typeof(IGeneratorLogFactory),typeof(GeneratorLogFactory));

            //MQ
            services.AddTransient(typeof(IRabbitMQFactory<BaseRequest, BaseResponse>), typeof(RabbitMQFactory<BaseRequest, BaseResponse>));
            #endregion

            //Repo
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddTransient(typeof(IRepositoryFactory<>), typeof(RepositoryFactory<>));

            //Mapper
            //services.AddTransient<IUsuarioMapper<Usuario, CreateUsuarioRequest, CreateUsuarioResponse>, UsuarioMapper<Usuario, CreateUsuarioRequest, CreateUsuarioResponse>>();
            //services.AddTransient<IUsuarioMapper<Usuario, GetByCPFRequest, GetByCPFResponse>, UsuarioMapper<Usuario, GetByCPFRequest, GetByCPFResponse>>();
            //services.AddTransient<IUsuarioMapper<Usuario, SendInviteRequest, SendInviteResponse>, UsuarioMapper<Usuario, SendInviteRequest, SendInviteResponse>>();
            //services.AddTransient<IUsuarioMapper<Usuario, AuthenticationRequest, AuthenticationResponse>, UsuarioMapper<Usuario, AuthenticationRequest, AuthenticationResponse>>();
            //services.AddTransient<IUsuarioMapper<Usuario, VincularClienteRequest, VincularClienteResponse>, UsuarioMapper<Usuario, VincularClienteRequest, VincularClienteResponse>>();
            //services.AddTransient<IUsuarioAcessoMapper<UsuarioAcesso, VincularClienteRequestService, VincularClienteResponseService>, UsuarioAcessoMapper<UsuarioAcesso, VincularClienteRequestService, VincularClienteResponseService>>();

            //Services
            //services.AddTransient<IVincularClienteService<VincularClienteRequestService, VincularClienteResponseService>, VincularClienteService<VincularClienteRequestService, VincularClienteResponseService>>();

            //Handlers
            //services.AddTransient<IRequestHandler<VincularClienteRequest, VincularClienteResponse>, VincularClienteHandlers>();
            //services.AddTransient<IRequestHandler<CreateUsuarioRequest, CreateUsuarioResponse>, CreateUsuarioHandler>();
            //services.AddTransient<IRequestHandler<GetByCPFRequest, GetByCPFResponse>, GetByCPFHandler>();
            //services.AddTransient<IRequestHandler<SendInviteRequest, SendInviteResponse>, SendInviteHandler>();

            return services;
        }
    }
}
