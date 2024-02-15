using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Mapper.Infrastructure.Usuario;
using IP.Repository.Infrastructure.Usuarios;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class GetByCPFHandler : IRequestHandler<GetByCPFRequest, GetByCPFResponse>
    {
        IUsuarioRepository<Domain.Usuario> IUsuarioRepositories;
        IUsuarioMapper<Domain.Usuario, GetByCPFRequest, GetByCPFResponse> IMapper;

        public GetByCPFHandler(
            IUsuarioRepository<Domain.Usuario> iUsuarioRepositories,
            IUsuarioMapper<Domain.Usuario, GetByCPFRequest,
                GetByCPFResponse> iGenerateMapper)
        {
            IUsuarioRepositories = iUsuarioRepositories;
            IMapper = iGenerateMapper;
        }

        public Task<GetByCPFResponse> Handle(GetByCPFRequest request, CancellationToken cancellationToken)
        {
            var result = IUsuarioRepositories.Get(o => o.Cpf == request.Cpf,cancellationToken);
            var response = IMapper.DomainToResponse(result);
            return Task.FromResult(response).Result;
        }
    }
}
