using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;

namespace IP.Services.Usuario.Request
{
    public interface IServicesUsuarioRequest
    {
        Task<UsuarioGetByIdResponse> GetByIdAsync(UsuarioGetByIdRequest request, CancellationToken cancellationToken);
        Task<UsuarioGetByCPFResponse> GetByCPFAsync(UsuarioGetByCPFRequest request, CancellationToken cancellationToken);
        Task<UsuarioSendInviteResponse> SendInviteAsync(UsuarioSendInviteRequest request, CancellationToken cancellationToken);
        Task<UsuarioVincularClinicaResponse> VincularClinica(UsuarioVincularClinicaRequest request, CancellationToken cancellationToken);
    }
}
