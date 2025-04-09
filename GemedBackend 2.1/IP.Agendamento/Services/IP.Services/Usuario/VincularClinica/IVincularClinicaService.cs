using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;

namespace IP.Services.Usuario.VincularClinica
{
    public interface IVincularClinicaService
    {
        Task<UsuarioVincularClinicaResponse> VincularClinica();
    }
}
