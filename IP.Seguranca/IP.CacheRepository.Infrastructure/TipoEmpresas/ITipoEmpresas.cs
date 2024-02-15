using IP.BaseDomains;
using IP.MongoDb;


namespace IP.CacheRepository.Infrastructure.TipoEmpresas
{
    public interface ITipoEmpresas<T, MongoConfig> : IMongoDb<T, MongoConfig>
        where T : BaseDomain
        where MongoConfig : MongoDbConfig
    {
    }
}
