using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Cryptography.CryptographyFactory;
using IP.Logs.LogsFactory;
using IP.Mails.MailsFactory;
using IP.Mapper.MapperFactory;
using IP.MongoDb.Domains;
using IP.MongoDb.MongoDbFactory;
using IP.RabbitMQ.RabbitMQFactory;
using IP.Services.Usuario.Authentication;
using IP.Services.Usuario.Create;
using IP.Services.Usuario.GetByCPF;
using IP.Services.Usuario.GetById;
using IP.Services.Usuario.SendInvite;
using IP.Services.Usuario.VincularClinica;
using IP.UnitEntityFramework.UnitEntityFramework.RepositoryFactory;

namespace IP.Services
{
    public class ServicesFactory : IServicesFactory
    {
        public async Task<AuthenticationService> Create(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var mapper = new MapperFactory<Domains.Domain.Usuario, AuthenticationRequest, AuthenticationResponse>();

            return new AuthenticationService(
                request,
                cancellationToken,
                new CryptographyFactory(),
                new RepositoryFactory<Domains.Domain.Usuario>(),
                new MapperFactory<Domains.Domain.Usuario, AuthenticationRequest, AuthenticationResponse>(),
                new LogsFactory());
        }

        public async Task<GetByCPFService> Create(UsuarioGetByCPFRequest request, CancellationToken cancellationToken)
        {
            return new GetByCPFService(
                request,
                cancellationToken,
                new RepositoryFactory<Domains.Domain.Usuario>(),
                new MapperFactory<Domains.Domain.Usuario, UsuarioGetByCPFRequest, UsuarioGetByCPFResponse>(),
                new LogsFactory());
        }

        public async Task<GetByIdService> Create(UsuarioGetByIdRequest request, CancellationToken cancellationToken)
        {

            return new GetByIdService(
                request,
                cancellationToken,
                new MapperFactory<Domains.Domain.Usuario, UsuarioGetByIdRequest, UsuarioGetByIdResponse>(),
                new LogsFactory(),
                new RepositoryFactory<Domains.Domain.Usuario>());
        }

        public async Task<SendInviteService> Create(UsuarioSendInviteRequest request, CancellationToken cancellationToken)
        {
            return new SendInviteService(
                request,
                cancellationToken,
                new CryptographyFactory(),
                new MongoDbFactory<TipoEmail>(),
                new MailsFactory(),
                new LogsFactory());
        }

        public async Task<CreateService> Create(UsuarioCreateRequest request, CancellationToken cancellationToken)
        {
            return new CreateService(
                request,
                cancellationToken,
                new LogsFactory(),
                new RepositoryFactory<Domains.Domain.Usuario>(),
                new MapperFactory<Domains.Domain.Usuario, UsuarioCreateRequest, UsuarioCreateResponse>());
        }

        public async Task<VincularClinicaService> Create(UsuarioVincularClinicaRequest request, CancellationToken cancellationToken)
        {
            return new VincularClinicaService(
                request,
                cancellationToken,
                new RabbitMQFactory<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>(),
                new LogsFactory());
        }

        public async Task<LogsFactory> CreateLogs()
        {
            return new LogsFactory();
        }
    }
}
