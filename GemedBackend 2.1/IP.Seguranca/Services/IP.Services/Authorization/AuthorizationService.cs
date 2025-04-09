using IP.Application.Comands.Requests.Authorization;
using IP.Application.Comands.Responses.Authorization;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.Domains;
using IP.MongoDb.MongoDbConfig;
using IP.MongoDb.MongoDbFactory;
using MongoDB.Driver;

namespace IP.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly AuthorizationRequest request;
        private readonly CancellationToken cancellationToken;
        private readonly ILogsFactory iLogsFactory;
        private readonly IMongoDbFactory<Autenticacoes> iMongoDbFactory;


        public AuthorizationService(
            AuthorizationRequest request,
            CancellationToken cancellationToken,
            ILogsFactory iLogsFactory,
            IMongoDbFactory<Autenticacoes> iMongoDbFactory)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iLogsFactory = iLogsFactory;
            this.iMongoDbFactory = iMongoDbFactory;
        }

        public async Task<AuthorizationResponse> AuthorizationAsync()
        {
            var result = new AuthorizationResponse();
            try
            {
                var mongoConfig = new MongoConfig() { Collection = "Autenticacoes" };
                var mongoDb = iMongoDbFactory.Create(mongoConfig);

                var autenticacao = mongoDb.Get(Builders<Autenticacoes>.Filter.Where(o => o.Id.ToString() == request.token), cancellationToken).Result;
                if (autenticacao == null || !autenticacao.Logado)
                {
                    result.Message = "Usuario nao Logado";
                    return result;
                }
                else if (autenticacao.UltimoLogin > DateTime.UtcNow.AddMinutes(20))
                {
                    autenticacao.Logado = false;
                    _ = mongoDb.Update(autenticacao, cancellationToken);
                    result.Message = "Autenticao Expirou";
                    return result;
                }

                result.Id = autenticacao.IdUsuario;
                result.Token = autenticacao.Id.ToString();
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
