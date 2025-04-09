using IP.BaseDomains;
using IP.MongoDb.MongoDb;
using IP.MongoDb.MongoDbConfig;

namespace IP.MongoDb.MongoDbFactory
{
    public interface IMongoDbFactory<T> where T : BaseDomainMongo
    {
        MongoDb<T> Create(MongoConfig config);
    }
}
