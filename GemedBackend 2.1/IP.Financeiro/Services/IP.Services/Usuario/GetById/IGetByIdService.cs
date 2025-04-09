using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;

namespace IP.Services.Usuario.GetById
{
    public interface IGetByIdService
    {
        Task<UsuarioGetByIdResponse> GetByIdAsync();
    }
}
