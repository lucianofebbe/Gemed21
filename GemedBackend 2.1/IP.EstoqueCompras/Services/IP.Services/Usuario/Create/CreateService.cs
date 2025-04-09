using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mapper.MapperFactory;
using IP.MongoDb.Domains;
using IP.MongoDb.MongoDbFactory;
using IP.Repository.Infrastructure.Contexts;
using IP.UnitEntityFramework.UnitEntityFramework.RepositoryFactory;

namespace IP.Services.Usuario.Create
{
    public class CreateService : ICreateService
    {
        private readonly UsuarioCreateRequest request;
        private readonly CancellationToken cancellationToken;
        private readonly ILogsFactory iLogsFactory;
        private readonly IRepositoryFactory<Domains.Domain.Usuario> iRepositoryFactory;
        private readonly IMapperFactory<Domains.Domain.Usuario, UsuarioCreateRequest, UsuarioCreateResponse> iMapperFactory;

        public CreateService(
            UsuarioCreateRequest request,
            CancellationToken cancellationToken,
            ILogsFactory iLogsFactory,
            IRepositoryFactory<Domains.Domain.Usuario> iRepositoryFactory,
            IMapperFactory<Domains.Domain.Usuario, UsuarioCreateRequest, UsuarioCreateResponse> iMapperFactory)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iLogsFactory = iLogsFactory;
            this.iRepositoryFactory = iRepositoryFactory;
            this.iMapperFactory = iMapperFactory;
        }

        public async Task<UsuarioCreateResponse> CreateUsuarioAsync()
        {
            try
            {
                var repositorio = iRepositoryFactory.CreateCommands(new ContextDefault(request.ConnectionString, request.ProviderName), cancellationToken);
                var mapper = iMapperFactory.Create();
                var usuario = mapper.RequestToDomain(request).Result;
                if (usuario.IdUsuario > 0)
                    return mapper.DomainToResponse(repositorio.Insert(usuario, cancellationToken).Result).Result;
                else
                    return mapper.DomainToResponse(repositorio.Update(usuario, cancellationToken).Result).Result;
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.Create(new LogsSettings(false, TypeLogs.System)).GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
