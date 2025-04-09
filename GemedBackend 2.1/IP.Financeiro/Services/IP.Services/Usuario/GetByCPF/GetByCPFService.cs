using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mapper.MapperFactory;
using IP.Repository.Infrastructure.Contexts;
using IP.UnitEntityFramework.UnitEntityFramework.RepositoryFactory;

namespace IP.Services.Usuario.GetByCPF
{
    public class GetByCPFService : IGetByCPFService
    {
        private readonly UsuarioGetByCPFRequest request;
        private readonly CancellationToken cancellationToken;
        private readonly IRepositoryFactory<Domains.Domain.Usuario> iRepositoryFactory;
        private readonly IMapperFactory<Domains.Domain.Usuario, UsuarioGetByCPFRequest, UsuarioGetByCPFResponse> iMapperGetByCPF;
        private readonly ILogsFactory iLogsFactory;
        public GetByCPFService(
            UsuarioGetByCPFRequest request,
            CancellationToken cancellationToken,
            IRepositoryFactory<Domains.Domain.Usuario> iRepositoryFactory,
            IMapperFactory<Domains.Domain.Usuario, UsuarioGetByCPFRequest, UsuarioGetByCPFResponse> iMapperGetByCPF,
            ILogsFactory iLogsFactory)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iMapperGetByCPF = iMapperGetByCPF;
            this.iLogsFactory = iLogsFactory;
            this.iRepositoryFactory = iRepositoryFactory;
        }

        public async Task<UsuarioGetByCPFResponse> GetByCPFAsync()
        {
            try
            {
                var repositorio = iRepositoryFactory.CreateRequests(new ContextDefault(request.ConnectionString, request.ProviderName), cancellationToken);
                var result = repositorio.Get(o => o.Cpf == request.Cpf, cancellationToken).Result;
                var mapper = iMapperGetByCPF.Create();
                var responseMapper = mapper.DomainToResponse(result);
                return await responseMapper;

            }
            catch (Exception ex)
            {
                _ = iLogsFactory.Create(new LogsSettings(false, TypeLogs.System)).GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
