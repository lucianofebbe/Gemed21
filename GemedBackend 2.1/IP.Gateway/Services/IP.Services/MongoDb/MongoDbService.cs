using IP.Application.Comands.Requests;
using IP.Application.Comands.Responses;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.Domains;
using IP.MongoDb.MongoDb;
using IP.MongoDb.MongoDbConfig;
using IP.MongoDb.MongoDbFactory;
using MongoDB.Driver;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace IP.Services.MongoDb
{
    public class MongoDbService : IMongoDbServices
    {
        private readonly string tipoRequisicao;
        private readonly string tipoEmpresa;
        private readonly string empresa;
        private readonly CancellationToken cancellationToken;
        private readonly IMongoDbFactory<TipoRequisicao> iMongoDbFactoryTipoRequisicao;
        private readonly IMongoDbFactory<TipoEmpresa> iMongoDbFactoryipoEmpresa;
        private readonly ILogsFactory iLogsFactory;
        public MongoDbService(
            string tipoRequisicao,
            string tipoEmpresa,
            string empresa,
            CancellationToken cancellationToken,
            IMongoDbFactory<TipoRequisicao> iMongoDbFactoryTipoRequisicao,
            IMongoDbFactory<TipoEmpresa> iMongoDbFactoryipoEmpresa,
            ILogsFactory iLogsFactory)
        {
            this.tipoRequisicao = tipoRequisicao;
            this.tipoEmpresa = tipoEmpresa;
            this.empresa = empresa;
            this.cancellationToken = cancellationToken;
            this.iMongoDbFactoryTipoRequisicao = iMongoDbFactoryTipoRequisicao;
            this.iMongoDbFactoryipoEmpresa = iMongoDbFactoryipoEmpresa;
            this.iLogsFactory = iLogsFactory;
        }

        public async Task<Tuple<TipoRequisicao, TipoEmpresa>> GetTypeAndCompanyAsync()
        {
            try
            {
                MongoDb<TipoRequisicao> repoTipoRequisicao = null;
                MongoDb<TipoEmpresa> repoTipoEmpresa = null;
                TipoRequisicao _tipoRequisicao = null;
                TipoEmpresa _tipoEmpresa = null;

#if DEBUG
                repoTipoRequisicao = iMongoDbFactoryTipoRequisicao.Create(new MongoConfig() { Collection = "TipoRequisicao", ConnectionString = "mongodb://Gemed2022:Gemed2022Teste@localhost:27018/?authSource=admin" });
                repoTipoEmpresa = iMongoDbFactoryipoEmpresa.Create(new MongoConfig() { Collection = "TipoEmpresa", ConnectionString = "mongodb://Gemed2022:Gemed2022Teste@localhost:27018/?authSource=admin" });
#else
                repoTipoRequisicao = iMongoDbFactoryTipoRequisicao.Create(new MongoConfig() { Collection = "TipoRequisicao" });
                repoTipoEmpresa = iMongoDbFactoryipoEmpresa.Create(new MongoConfig() { Collection = "TipoEmpresa" });
#endif

                if (tipoRequisicao == "Autenticacao")
                {
                    _tipoRequisicao = repoTipoRequisicao
                        .Get(Builders<TipoRequisicao>.Filter.Where(o => o.Type == "Autenticacao"), cancellationToken).Result;

                    _tipoEmpresa = repoTipoEmpresa
                        .Get(Builders<TipoEmpresa>.Filter.Where(o => o.Type == "Autenticacao"), cancellationToken).Result;

                }
                else if (tipoRequisicao == "Autorizacao")
                {
                    _tipoRequisicao = repoTipoRequisicao
                        .Get(Builders<TipoRequisicao>.Filter.Where(o => o.Type == "Autorizacao"), cancellationToken).Result;

                    _tipoEmpresa = repoTipoEmpresa
                        .Get(Builders<TipoEmpresa>.Filter.Where(o => o.Type == "Autenticacao"), cancellationToken).Result;
                }
                else
                {
                    _tipoRequisicao = repoTipoRequisicao
                        .Get(Builders<TipoRequisicao>.Filter.Where(o => o.Type == tipoRequisicao), cancellationToken).Result;

                    _tipoEmpresa = repoTipoEmpresa
                        .Get(Builders<TipoEmpresa>.Filter.Where(o => o.Type == tipoEmpresa && o.Company == empresa), cancellationToken).Result;
                }

                return new Tuple<TipoRequisicao, TipoEmpresa>(_tipoRequisicao, _tipoEmpresa);
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
