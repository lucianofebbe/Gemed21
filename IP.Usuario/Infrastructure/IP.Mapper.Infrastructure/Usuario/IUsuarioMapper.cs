using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Mapper.Mapper;

namespace IP.Mapper.Infrastructure.Usuario
{
    public interface IUsuarioMapper :
        IMapper<Domain.Usuario, UsuarioCreateRequest, UsuarioCreateResponse>
    {
    }
}
