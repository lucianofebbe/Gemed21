using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mapper.Mapper;
using IP.Mapper.MapperFactory;
using IP.Repository.Infrastructure.RepositoryFactory;
using IP.Repository.Infrastructure.Unit.Commands;

namespace IP.Services.Usuario.Commands
{
    public class ServicesUsuarioCommands : IServicesUsuarioCommands
    {
        private readonly UnitOfWorkCommands<Domain.Usuario> iRepositoryFactory;
        private readonly Mapper<Domain.Usuario, UsuarioCreateRequest, UsuarioCreateResponse> iMapperFactory;
        private readonly Logs.Logs.Logs iLogsFactory;

        public ServicesUsuarioCommands(IRepositoryFactory<Domain.Usuario> iRepositoryFactory,
            IMapperFactory<Domain.Usuario, UsuarioCreateRequest, UsuarioCreateResponse> iMapperFactory,
            ILogsFactory iLogsFactory)
        {
            this.iRepositoryFactory = iRepositoryFactory.CreateCommands(new CancellationToken());
            this.iMapperFactory = iMapperFactory.Create();
            this.iLogsFactory = iLogsFactory.Create(new LogsSettings() { SendMail = true });
        }


        public async Task<UsuarioCreateResponse> CreateUsuarioAsync(UsuarioCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var toDomain = iMapperFactory.RequestToDomain(request).Result;
                var result = await iRepositoryFactory.Add(toDomain, cancellationToken);
                var toResponse = iMapperFactory.DomainToResponse(result).Result;
                return toResponse;
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
