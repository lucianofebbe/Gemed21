using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Requests.Authorization;
using IP.Logs.LogsFactory;
using IP.MongoDb.Domains;
using IP.MongoDb.MongoDbFactory;
using IP.Services.Authentication;
using IP.Services.Authorization;

namespace IP.Services
{
    public class ServicesFactory : IServicesFactory
    {
        public async Task<AuthenticationService> Create(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            return new AuthenticationService(
                request,
                cancellationToken,
                new LogsFactory(),
                new MongoDbFactory<Autenticacoes>());
        }

        public async Task<AuthorizationService> Create(AuthorizationRequest request, CancellationToken cancellationToken)
        {
            return new AuthorizationService(
                request,
                cancellationToken,
                new LogsFactory(),
                new MongoDbFactory<Autenticacoes>());
        }

        public async Task<LogsFactory> CreateLogs()
        {
            return new LogsFactory();
        }
    }
}
