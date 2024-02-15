using IP.BaseDomains;
using IP.MongoDb.MongoDb;

namespace IP.MongoDb.MongoDbFactory
{
    public interface IMongoDbFactory<T> where T : BaseDomainMongo
    {
        MongoDb<T> Create(string collection, string database = "DbCache");
    }
}
