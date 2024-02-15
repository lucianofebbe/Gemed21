using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.GeneratorFactory;
using IP.Logs.GeneratorLog;
using IP.Logs.GeneratorLogSettings;
using IP.Mapper.Infrastructure.Usuario;
using IP.Repository.Infrastructure.RepositoryFactory;
using IP.Repository.Infrastructure.Unit;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        IUnitOfWork<Domain.Usuario> IUsuarioRepositories;
        IUsuarioMapper<Domain.Usuario, GetByIdRequest, GetByIdResponse> IMapper;
        GeneratorLog log;

        public GetByIdHandler(IRepositoryFactory<Domain.Usuario> iUsuarioRepositories,
            IUsuarioMapper<Domain.Usuario, GetByIdRequest, GetByIdResponse> iGenerateMapper,
            IGeneratorLogFactory log)
        {
            IUsuarioRepositories = iUsuarioRepositories.Create();
            IMapper = iGenerateMapper;
            this.log = log.Create(new LogSettings() { SendMail = true });
        }

        public Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = IUsuarioRepositories.Get(o => o.Cpf == request.Id, cancellationToken);
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
