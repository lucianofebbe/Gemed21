using IP.Application.Comands.Responses.Authorization;

namespace IP.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResponse> AuthorizationAsync();
    }
}
