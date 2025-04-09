using IP.Application.Comands.Requests.Authorization;
using IP.Application.Comands.Responses.Authorization;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers.Authorization
{
    public class AuthorizationHandler : IRequestHandler<AuthorizationRequest, AuthorizationResponse>
    {
        private readonly IServicesFactory iService;

        public AuthorizationHandler(
            IServicesFactory iService)
        {
            this.iService = iService;
        }

        public async Task<AuthorizationResponse> Handle(AuthorizationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var service = iService.Create(request, cancellationToken).Result;
                return await service.AuthorizationAsync();
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
