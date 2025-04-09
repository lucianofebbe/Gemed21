using IP.BaseDomains;
using IP.MongoDb.MongoDb;
using IP.MongoDb.MongoDbConfig;

namespace IP.MongoDb.MongoDbFactory
{
    public class MongoDbFactory<T> : IMongoDbFactory<T> where T : BaseDomainMongo
    {
        public MongoDb<T> Create(MongoConfig config)
        {
            return new MongoDb<T>(config);
        }
    }
}
