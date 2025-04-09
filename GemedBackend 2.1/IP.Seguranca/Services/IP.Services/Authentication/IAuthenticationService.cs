using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;

namespace IP.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticationAsync();
    }
}
