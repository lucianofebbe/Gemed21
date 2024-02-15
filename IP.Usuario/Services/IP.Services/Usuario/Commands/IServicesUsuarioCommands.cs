using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;

namespace IP.Services.Usuario.Commands
{
    public interface IServicesUsuarioCommands
    {
        Task<UsuarioCreateResponse> CreateUsuarioAsync(UsuarioCreateRequest request, CancellationToken cancellationToken);
    }
}
