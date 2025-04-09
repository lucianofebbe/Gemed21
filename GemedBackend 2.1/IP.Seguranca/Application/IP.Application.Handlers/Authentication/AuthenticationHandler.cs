using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers.Authentication
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
                _ = service.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new ApplicationException("", ex);
            }
        }
    }
}
