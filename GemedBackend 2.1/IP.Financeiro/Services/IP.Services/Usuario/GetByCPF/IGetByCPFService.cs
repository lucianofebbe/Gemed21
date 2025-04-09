using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;

namespace IP.Services.Usuario.GetByCPF
{
    public interface IGetByCPFService
    {
        Task<UsuarioGetByCPFResponse> GetByCPFAsync();
    }
}
