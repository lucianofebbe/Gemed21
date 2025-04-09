using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;

namespace IP.Services.Usuario.SendInvite
{
    public interface ISendInviteService
    {
        Task<UsuarioSendInviteResponse> SendInviteAsync();
    }
}
