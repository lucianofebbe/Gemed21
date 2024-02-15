using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Cryptography.TokenCryptography;
using IP.DomainMongo;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mails.MailsFactory;
using IP.Mapper.Mapper;
using IP.Mapper.MapperFactory;
using IP.MongoDb.MongoDb;
using IP.MongoDb.MongoDbFactory;
using IP.RabbitMQ.RabbitMQFactory;
using IP.RabbitMQ.RabbitMQSettings;
using IP.RabbitMQ.RequestReply;
using IP.Repository.Infrastructure.RepositoryFactory;
using IP.Repository.Infrastructure.Unit.Request;
using IP.Services.Cliente.Request;
using MongoDB.Driver;

namespace IP.Services.Usuario.Request
{
    public class ServicesUsuarioRequest : IServicesUsuarioRequest
    {
        private readonly UnitOfWorkRequest<Domain.Usuario> iRepositoryFactory;
        private readonly Mapper<Domain.Usuario, UsuarioGetByCPFRequest, UsuarioGetByCPFResponse> iMapperGetByCPF;
        private readonly Mapper<Domain.Usuario, UsuarioGetByIdRequest, UsuarioGetByIdResponse> iMapperGetById;
        private readonly Mapper<Domain.Usuario, UsuarioSendInviteRequest, UsuarioSendInviteResponse> iMapperSendInvite;
        private readonly Logs.Logs.Logs iLogsFactory;
        private readonly Mails.Mails.Mails iMailsFactory;
        private readonly ITokenCryptography iTokenCryptography;
        private readonly MongoDb<TipoEmail> iMongoDbFactory;
        private readonly IServicesClienteRequest iServicesClienteRequest;
        private readonly IRabbitMQFactory<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse> iRabbitMQFactory;

        public ServicesUsuarioRequest(
            IRepositoryFactory<Domain.Usuario> iRepositoryFactory,
            IMapperFactory<Domain.Usuario, UsuarioGetByCPFRequest, UsuarioGetByCPFResponse> iMapperGetByCPF,
            IMapperFactory<Domain.Usuario, UsuarioGetByIdRequest, UsuarioGetByIdResponse> iMapperGetById,
            IMapperFactory<Domain.Usuario, UsuarioSendInviteRequest, UsuarioSendInviteResponse> iMapperSendInvite,
            ILogsFactory iLogsFactory,
            IMailsFactory iMailsFactory,
            ITokenCryptography iTokenCryptography,
            IMongoDbFactory<TipoEmail> iMongoDbFactory,
            IServicesClienteRequest iServicesClienteRequest,
            IRabbitMQFactory<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse> iRabbitMQFactory)
        {
            this.iMapperGetByCPF = iMapperGetByCPF.Create();
            this.iMapperGetById = iMapperGetById.Create();
            this.iMapperSendInvite = iMapperSendInvite.Create();
            this.iRepositoryFactory = iRepositoryFactory.CreateGets(new CancellationToken());
            this.iLogsFactory = iLogsFactory.Create(new LogsSettings() { SendMail = false });
            this.iMailsFactory = iMailsFactory.Create();
            this.iTokenCryptography = iTokenCryptography;
            this.iMongoDbFactory = iMongoDbFactory.Create("TipoEmail");
            this.iServicesClienteRequest = iServicesClienteRequest;
            this.iRabbitMQFactory = iRabbitMQFactory;
        }

        public async Task<UsuarioGetByCPFResponse> GetByCPFAsync(UsuarioGetByCPFRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = iRepositoryFactory.Get(o => o.Cpf == request.Cpf, cancellationToken).Result;
                var responseMapper = iMapperGetByCPF.DomainToResponse(result);
                return await responseMapper;

            }
            catch (Exception ex)
            {
                _ = iLogsFactory.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }

        public async Task<UsuarioGetByIdResponse> GetByIdAsync(UsuarioGetByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = iRepositoryFactory.Get(o => o.Id == request.Id, cancellationToken).Result;
                var responseMapper = iMapperGetById.DomainToResponse(result);
                return await responseMapper;

            }
            catch (Exception ex)
            {
                _ = iLogsFactory.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }

        public async Task<UsuarioSendInviteResponse> SendInviteAsync(UsuarioSendInviteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var token = $"cpf={request.Cpf}:clienteId={request.ClienteId}";
                var tokenCripted = iTokenCryptography.GenerateToken(token).Result;
                var mailText = iMongoDbFactory.Get(Builders<TipoEmail>.Filter.Where(o => o.Type == "SendInvite"), cancellationToken).Result.Text;
                mailText.Replace("{{SEU_TOKEN}}", tokenCripted);

                _ = iMailsFactory.SendMail(new Mails.MailsSettings.MailsSettings()
                {
                    Provider = "Hotmail",
                    Subject = "New Token invite",
                    BodyHtml = true,
                    Body = mailText,
                    ToEmail = request.Email
                });
                return await Task.FromResult(new UsuarioSendInviteResponse() { Send = true });
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.GenerateLog(ex);
                return await Task.FromResult(new UsuarioSendInviteResponse() { Send = false });
            }
        }

        public async Task<UsuarioVincularClinicaResponse> VincularClinica(UsuarioVincularClinicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //var cliente = await iServicesClienteRequest.GetByIdAsync(new Application.Comands.Requests.Cliente.ClienteGetByIdRequest() { Id = request.ClienteId }, cancellationToken);

                var listRequest = new List<UsuarioVincularClinicaRequest>
                {
                    request
                };

                var settings = new RabbitMQSettings<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>()
                {
                    HostName = "http://localhost:9092/",
                    Requests = listRequest,
                    Exchange = "Direct_Exchange",
                    AutoDelete = false,
                    Exclusive = true,
                    QueueName = "UsuarioVincularClinica",
                    RoutingKey = "UsuarioVincular"
                };

                var requestReply = iRabbitMQFactory.CreateRequestReply();
                _ = requestReply.Send(settings);
                return new UsuarioVincularClinicaResponse() { };
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.GenerateLog(ex);
                return await Task.FromResult(new UsuarioVincularClinicaResponse());
            }
        }
    }
}
