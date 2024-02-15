using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Mapper.Infrastructure.Usuario;
using IP.Repository.Infrastructure.Usuarios;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        IUsuarioRepository<Domain.Usuario> IUsuarioRepositories;
        IUsuarioMapper<Domain.Usuario, GetByIdRequest, GetByIdResponse> IMapper;

        public GetByIdHandler(IUsuarioRepository<Domain.Usuario> iUsuarioRepositories,
            IUsuarioMapper<Domain.Usuario, GetByIdRequest,
                GetByIdResponse> iGenerateMapper)
        {
            IUsuarioRepositories = iUsuarioRepositories;
            IMapper = iGenerateMapper;
        }

        public Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var result = IUsuarioRepositories.Get(o => o.Cpf == request.Id, cancellationToken);
            var response = IMapper.DomainToResponse(result);
            return Task.FromResult(response).Result;
        }
    }
}
