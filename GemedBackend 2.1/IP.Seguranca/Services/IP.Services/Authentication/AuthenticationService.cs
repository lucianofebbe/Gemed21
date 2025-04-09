using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.Domains;
using IP.MongoDb.MongoDbConfig;
using IP.MongoDb.MongoDbFactory;
using MongoDB.Bson;

namespace IP.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationRequest request;
        private readonly CancellationToken cancellationToken;
        private readonly ILogsFactory iLogsFactory;
        private readonly IMongoDbFactory<Autenticacoes> iMongoDbFactory;


        public AuthenticationService(
            AuthenticationRequest request,
            CancellationToken cancellationToken,
            ILogsFactory iLogsFactory,
            IMongoDbFactory<Autenticacoes> iMongoDbFactory)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iLogsFactory = iLogsFactory;
            this.iMongoDbFactory = iMongoDbFactory;
        }

        public async Task<AuthenticationResponse> AuthenticationAsync()
        {
            var result = new AuthenticationResponse();
            try
            {
                var autenticacao = new Autenticacoes()
                {
                    IdUsuario = request.Id,
                    Dispositivo = "Web",
                    Logado = true,
                    RefreshToken = ObjectId.GenerateNewId().ToString(),
                    UltimoLogin = DateTime.UtcNow
                };

                var mongoConfig = new MongoConfig() { Collection = "Autenticacoes" };
                var mongoDb = iMongoDbFactory.Create(mongoConfig);
                result.Token = mongoDb.Insert(autenticacao, cancellationToken).Result;
                result.RefreshToken = autenticacao.RefreshToken;
                return result;
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                result.Message = ex.InnerException.Message;
                return result;
            }
        }
    }
}
