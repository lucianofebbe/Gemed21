using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Mapper.Infrastructure.Usuario;
using IP.Repository.Infrastructure.Usuarios;
using MediatR;

namespace IP.Application.Handlers
{
    public class CreateUsuarioHandler : IRequestHandler<CreateUsuarioRequest, CreateUsuarioResponse>
    {
        IUsuarioRepository<Domain.Usuario> IRepositories;
        IUsuarioMapper<Domain.Usuario, CreateUsuarioRequest, CreateUsuarioResponse> IMapper;
        public CreateUsuarioHandler(
            IUsuarioRepository<Domain.Usuario> iUsuarioRepositories,
            IUsuarioMapper<Domain.Usuario, CreateUsuarioRequest,
                CreateUsuarioResponse> iGenerateMapper)
        {
            IRepositories = iUsuarioRepositories;
            IMapper = iGenerateMapper;
        }

        public Task<CreateUsuarioResponse> Handle(CreateUsuarioRequest request, CancellationToken cancellationToken)
        {
            var usuario = new Domain.Usuario();
            var result = IRepositories.Add(usuario, cancellationToken);
            var response = IMapper.DomainToResponse(result);
            return Task.FromResult(response).Result;
        }
    }
}
