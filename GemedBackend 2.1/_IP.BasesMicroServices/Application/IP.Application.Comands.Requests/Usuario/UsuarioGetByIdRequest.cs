using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Usuario
{
    public class UsuarioGetByIdRequest : BaseRequest, IRequest<UsuarioGetByIdResponse>
    {
    }
}
