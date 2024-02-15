using IP.Application.Comands.Requests.Cliente;
using IP.Application.Comands.Responses.Cliente;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mapper.Mapper;
using IP.Mapper.MapperFactory;
using IP.Repository.Infrastructure.RepositoryFactory;
using IP.Repository.Infrastructure.Unit.Request;

namespace IP.Services.Cliente.Request
{
    public class ServicesClienteRequest : IServicesClienteRequest
    {
        private readonly UnitOfWorkRequest<Domain.Cliente> iRepositoryFactory;
        private readonly Mapper<Domain.Cliente, ClienteGetByIdRequest, ClienteGetByIdResponse> iMapperGetById;
        private readonly Logs.Logs.Logs iLogsFactory;

        public ServicesClienteRequest(
            IRepositoryFactory<Domain.Cliente> iRepositoryFactory,
            IMapperFactory<Domain.Cliente, ClienteGetByIdRequest, ClienteGetByIdResponse> iMapperGetById,
            ILogsFactory iLogsFactory)
        {
            this.iRepositoryFactory = iRepositoryFactory.CreateGets(new CancellationToken());
            this.iMapperGetById = iMapperGetById.Create();
            this.iLogsFactory = iLogsFactory.Create(new LogsSettings() { SendMail = true });
        }

        public async Task<ClienteGetByIdResponse> GetByIdAsync(ClienteGetByIdRequest request, CancellationToken cancellationToken)
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
    }
}
