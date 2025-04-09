using IP.Application.Comands.Requests;
using IP.Logs.LogsFactory;
using IP.Services.Gateway;
using IP.Services.MongoDb;
using IP.Services.Requests;

namespace IP.Services
{
    public interface IServicesFactory
    {
        Task<GatewayService> CreateGatewayService(Request request, CancellationToken cancellationToken);
        Task<RequestService> CreateRequestService(Request request, CancellationToken cancellationToken);
        Task<MongoDbService> CreateMongoDbService(string tipoRequisicao, string tipoEmpresa, string empresa, CancellationToken cancellationToken);
        Task<LogsFactory> CreateLogs();
    }
}
