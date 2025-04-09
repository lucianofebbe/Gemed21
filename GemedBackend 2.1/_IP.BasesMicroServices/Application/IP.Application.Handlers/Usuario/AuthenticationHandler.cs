using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsSettings;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IServicesFactory iService;

        public AuthenticationHandler(
            IServicesFactory iService)
        {
            this.iService = iService;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var service = iService.Create(request, cancellationToken).Result;
                return await service.AuthenticationAsync();
            }
            catch (Exception ex)
            {
                var service = iService.CreateLogs().Result;

                _ = service.Create(new LogsSettings(false, TypeLogs.System)).GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
