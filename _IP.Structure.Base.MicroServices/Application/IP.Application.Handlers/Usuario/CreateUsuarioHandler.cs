using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.GeneratorFactory;
using IP.Logs.GeneratorLog;
using IP.Logs.GeneratorLogSettings;
using IP.Mapper.Infrastructure.Usuario;
using IP.Repository.Infrastructure.RepositoryFactory;
using IP.Repository.Infrastructure.Unit;
using MediatR;

namespace IP.Application.Handlers
{
    public class CreateUsuarioHandler : IRequestHandler<CreateUsuarioRequest, CreateUsuarioResponse>
    {
        UnitOfWork<Domain.Usuario> IRepositories;
        IUsuarioMapper<Domain.Usuario, CreateUsuarioRequest, CreateUsuarioResponse> IMapper;
        GeneratorLog log;
        public CreateUsuarioHandler(
            IRepositoryFactory<Domain.Usuario> iUsuarioRepositories,
            IUsuarioMapper<Domain.Usuario, CreateUsuarioRequest, CreateUsuarioResponse> iGenerateMapper,
            IGeneratorLogFactory log)
        {
            IRepositories = iUsuarioRepositories.Create();
            IMapper = iGenerateMapper;
            this.log = log.Create(new LogSettings() { SendMail = true });
        }

        public Task<CreateUsuarioResponse> Handle(CreateUsuarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = new Domain.Usuario();
                var result = IRepositories.Add(usuario, cancellationToken);
                var response = IMapper.DomainToResponse(result);
                return Task.FromResult(response).Result;
            }
            catch (Exception ex)
            {
                log.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
