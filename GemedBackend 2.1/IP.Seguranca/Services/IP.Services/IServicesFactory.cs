using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Requests.Authorization;
using IP.Logs.LogsFactory;
using IP.Services.Authentication;
using IP.Services.Authorization;

namespace IP.Services
{
    public interface IServicesFactory
    {
        Task<AuthenticationService> Create(AuthenticationRequest request, CancellationToken cancellationToken);
        Task<AuthorizationService> Create(AuthorizationRequest request, CancellationToken cancellationToken);
        Task<LogsFactory> CreateLogs();
    }
}
