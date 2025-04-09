using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mapper.MapperFactory;
using IP.MongoDb.MongoDbConfig;
using IP.Repository.Infrastructure.Contexts;
using IP.UnitEntityFramework.UnitEntityFramework.RepositoryFactory;

namespace IP.Services.Usuario.GetById
{
    public class GetByIdService : IGetByIdService
    {
        private readonly UsuarioGetByIdRequest request;
        private readonly CancellationToken cancellationToken;
        private readonly IMapperFactory<Domains.Domain.Usuario, UsuarioGetByIdRequest, UsuarioGetByIdResponse> iMapperGetById;
        private readonly ILogsFactory iLogsFactory;
        private readonly IRepositoryFactory<Domains.Domain.Usuario> iRepositoryFactory;

        public GetByIdService(
            UsuarioGetByIdRequest request,
            CancellationToken cancellationToken,
            IMapperFactory<Domains.Domain.Usuario, UsuarioGetByIdRequest, UsuarioGetByIdResponse> iMapperGetById,
            ILogsFactory iLogsFactory,
            IRepositoryFactory<Domains.Domain.Usuario> iRepositoryFactory
            )
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iMapperGetById = iMapperGetById;
            this.iLogsFactory = iLogsFactory;
            this.iRepositoryFactory = iRepositoryFactory;
        }
        public async Task<UsuarioGetByIdResponse> GetByIdAsync()
        {
            try
            {
                var repositorio = iRepositoryFactory.CreateRequests(new ContextDefault(request.ConnectionString, request.ProviderName), cancellationToken);
                var result = repositorio.Get(o => o.IdUsuario == request.Id, cancellationToken).Result;
                var mapper = iMapperGetById.Create();
                var responseMapper = mapper.DomainToResponse(result);
                return await responseMapper;

            }
            catch (Exception ex)
            {
                _ = iLogsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
