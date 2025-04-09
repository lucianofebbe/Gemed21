using IP.Application.Comands.Requests;
using IP.Application.Comands.Responses;
using IP.Domain;
using IP.Logs.LogsFactory;
using IP.Mapper.Infrastructure;
using IP.MongoDb.Domains;
using IP.MongoDb.MongoDbFactory;
using IP.Services.Gateway;
using IP.Services.MongoDb;
using IP.Services.Requests;

namespace IP.Services
{
    public class ServicesFactory : IServicesFactory
    {
        public async Task<GatewayService> CreateGatewayService(Request request, CancellationToken cancellationToken)
        {
            return new GatewayService(
                request,
                cancellationToken,
                new ServicesFactory(),
                new MapperGateway<AuthenticationResponse, Request, Response>(),
                new MapperGateway<AuthorizationResponse, Request, Response>());
        }

        public async Task<RequestService> CreateRequestService(Request request, CancellationToken cancellationToken)
        {
            return new RequestService(
                request,
                cancellationToken,
                new ServicesFactory());
        }

        public async Task<MongoDbService> CreateMongoDbService(string tipoRequisicao, string tipoEmpresa, string empresa, CancellationToken cancellationToken)
        {
            return new MongoDbService(
                tipoRequisicao,
                tipoEmpresa,
                empresa,
                cancellationToken,
                new MongoDbFactory<TipoRequisicao>(),
                new MongoDbFactory<TipoEmpresa>(),
                new LogsFactory());
        }

        public async Task<LogsFactory> CreateLogs()
        {
            return new LogsFactory();
        }
    }
}
