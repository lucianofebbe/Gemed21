using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;

namespace IP.Services.Usuario.Create
{
    public interface ICreateService
    {
        Task<UsuarioCreateResponse> CreateUsuarioAsync();
    }
}
