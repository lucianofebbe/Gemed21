using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
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
            var result = new AuthenticationResponse();
            try
            {
                var service = iService.Create(request, cancellationToken).Result;
                result = await service.AuthenticationAsync();
                return result;
            }
            catch (Exception ex)
            {
                var service = iService.CreateLogs().Result;

                _ = service.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                result.Message = ex.Message;
                return result;
            }
        }
    }
}
