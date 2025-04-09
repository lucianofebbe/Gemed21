using IP.MongoDb.Domains;

namespace IP.Services.MongoDb
{
    public interface IMongoDbServices
    {
        Task<Tuple<TipoRequisicao, TipoEmpresa>> GetTypeAndCompanyAsync();
    }
}
