using IP.BaseDomains;
using IP.MongoDb;
using MongoDB.Driver;

namespace IP.CacheRepository.Infrastructure.TipoEmpresas
{
    public class TipoEmpresas<T, MongoConfig> : ITipoEmpresas<T, MongoConfig>
        where T : BaseDomain
        where MongoConfig : MongoDbConfig
    {
        private IMongoDb<T, MongoConfig> MongoDb;
        public TipoEmpresas(IMongoDb<T, MongoConfig> mongoDb)
        {
            MongoDb = mongoDb;
        }


        public Task<bool> Delete(Task<T> model, Task<MongoConfig> config, CancellationToken cancellationToken)
        {
            return MongoDb.Delete(model, config, cancellationToken);
        }

        public Task<T> Get(Task<MongoConfig> config, FilterDefinition<T> FilterDefinition, CancellationToken cancellationToken)
        {
            return MongoDb.Get(config, FilterDefinition, cancellationToken);
        }

        public Task<List<T>> GetAll(Task<MongoConfig> config, FilterDefinition<T> FilterDefinition, CancellationToken cancellationToken)
        {
            return MongoDb.GetAll(config, FilterDefinition, cancellationToken);
        }

        public Task Insert(Task<T> model, Task<MongoConfig> config, CancellationToken cancellationToken)
        {
            return MongoDb.Insert(model, config, cancellationToken);
        }

        public Task<bool> Update(Task<T> model, Task<MongoConfig> config, CancellationToken cancellationToken)
        {
            return MongoDb.Update(model, config, cancellationToken);
        }
    }
}
