using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Requests.Cliente;
using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Authentication;
using IP.Application.Comands.Responses.Cliente;
using IP.Application.Comands.Responses.Usuario;
using IP.Application.Handlers;
using IP.Application.Handlers.Authentication;
using IP.Application.Handlers.Usuario;
using IP.Cryptography.AesCryptography;
using IP.Cryptography.PBKDF2Cryptography;
using IP.Cryptography.TokenCryptography;
using IP.Domain;
using IP.DomainMongo;
using IP.Logs.LogsConfig;
using IP.Logs.LogsFactory;
using IP.Mails.MailsConfig;
using IP.Mails.MailsFactory;
using IP.Mapper.MapperFactory;
using IP.MongoDb.MongoDbFactory;
using IP.RabbitMQ.RabbitMQFactory;
using IP.RabbitMQ.RequestReply;
using IP.Repository.Infrastructure.RepositoryFactory;
using IP.Services.Authentication.Commands;
using IP.Services.Authentication.Request;
using IP.Services.Cliente.Request;
using IP.Services.Usuario.Commands;
using IP.Services.Usuario.Request;
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

            #region IP.Infrastructure
            //Cryptography
            services.AddTransient<IAesCryptography, AesCryptography>();
            services.AddTransient<IJwtConfiguration, JwtConfiguration>();
            services.AddTransient<ITokenCryptography, TokenCryptography>();
            services.AddTransient<IPBKDF2Cryptography, PBKDF2Cryptography>();

            //MongoDataBase
            services.AddTransient<IMongoDbFactory<MailsConfig>, MongoDbFactory<MailsConfig>>();
            services.AddTransient<IMongoDbFactory<LogsConfig>, MongoDbFactory<LogsConfig>>();
            services.AddTransient<IMongoDbFactory<TipoEmpresa>, MongoDbFactory<TipoEmpresa>>();
            services.AddTransient<IMongoDbFactory<TipoEmail>, MongoDbFactory<TipoEmail>>();

            //Mails
            services.AddTransient<IMailsFactory, MailsFactory>();

            //Log
            services.AddTransient<ILogsFactory, LogsFactory>();
            #endregion

            //Repo
            services.AddTransient<IRepositoryFactory<Usuario>, RepositoryFactory<Usuario>>();
            services.AddTransient<IRepositoryFactory<UsuarioAcesso>, RepositoryFactory<UsuarioAcesso>>();
            services.AddTransient<IRepositoryFactory<Cliente>, RepositoryFactory<Cliente>>();

            #region Authentication
            //Mappers
            //services.AddTransient<IMapperFactory<Usuario, AuthenticationRequest, AuthenticationResponse>, MapperFactory<Usuario, AuthenticationRequest, AuthenticationResponse>>();

            //Services
            //services.AddTransient<IServicesAuthenticationRequests, ServicesAuthenticationRequests>();
            //services.AddTransient<IServicesAuthenticationCommands, ServicesAuthenticationCommands>();

            //Handlers
            //services.AddTransient<IRequestHandler<AuthenticationRequest, AuthenticationResponse>, AuthenticationHandler>();
            #endregion

            #region Cliente
            //Mappers
            services.AddTransient<IMapperFactory<Cliente, ClienteGetByIdRequest, ClienteGetByIdResponse>, MapperFactory<Cliente, ClienteGetByIdRequest, ClienteGetByIdResponse>>();

            //Services
            services.AddTransient<IServicesClienteRequest, ServicesClienteRequest>();

            //Handlers
            #endregion

            #region Usuarios
            //Rabbit
            services.AddTransient<IRabbitMQFactory<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>, RabbitMQFactory<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>>();

            //Mappers
            services.AddTransient<IMapperFactory<Usuario, UsuarioGetByIdRequest, UsuarioGetByIdResponse>, MapperFactory<Usuario, UsuarioGetByIdRequest, UsuarioGetByIdResponse>>();
            services.AddTransient<IMapperFactory<Usuario, UsuarioGetByCPFRequest, UsuarioGetByCPFResponse>, MapperFactory<Usuario, UsuarioGetByCPFRequest, UsuarioGetByCPFResponse>>();
            services.AddTransient<IMapperFactory<Usuario, UsuarioGetByIdRequest, UsuarioGetByIdResponse>, MapperFactory<Usuario, UsuarioGetByIdRequest, UsuarioGetByIdResponse>>();
            services.AddTransient<IMapperFactory<Usuario, UsuarioSendInviteRequest, UsuarioSendInviteResponse>, MapperFactory<Usuario, UsuarioSendInviteRequest, UsuarioSendInviteResponse>>();
            services.AddTransient<IMapperFactory<Usuario, UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>, MapperFactory<Usuario, UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>>();
            services.AddTransient<IMapperFactory<Usuario, UsuarioCreateRequest, UsuarioCreateResponse>, MapperFactory<Usuario, UsuarioCreateRequest, UsuarioCreateResponse>>();

            //Services
            services.AddTransient<IServicesUsuarioRequest, ServicesUsuarioRequest>();
            services.AddTransient<IServicesUsuarioCommands, ServicesUsuarioCommands>();

            //Handlers
            services.AddTransient<IRequestHandler<UsuarioCreateRequest, UsuarioCreateResponse>, CreateHandler>();
            services.AddTransient<IRequestHandler<UsuarioGetByCPFRequest, UsuarioGetByCPFResponse>, GetByCPFHandler>();
            services.AddTransient<IRequestHandler<UsuarioGetByIdRequest, UsuarioGetByIdResponse>, GetByIdHandler>();
            services.AddTransient<IRequestHandler<UsuarioSendInviteRequest, UsuarioSendInviteResponse>, SendInviteHandler>();
            services.AddTransient<IRequestHandler<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>, VincularClinicaHandler>();
            #endregion

            return services;
        }
    }
}
