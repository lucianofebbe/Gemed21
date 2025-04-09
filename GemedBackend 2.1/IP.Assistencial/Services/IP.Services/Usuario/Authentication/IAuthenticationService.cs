using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;

namespace IP.Services.Usuario.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticationAsync();
    }
}
