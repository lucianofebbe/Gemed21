using IP.BaseDomains;
using IP.MongoDb.MongoDb;
using IP.MongoDb.MongoDbConfig;

namespace IP.MongoDb.MongoDbFactory
{
    public class MongoDbFactory<T> : IMongoDbFactory<T> where T : BaseDomainMongo
    {
        private readonly string connectionString = "mongodb://Gemed2022:Gemed2022Teste@localhost:27018/?authSource=admin";

        public MongoDb<T> Create(string collection, string database = "DbCache")
        {
            return new MongoDb<T>(CreateConfig(collection, database));
        }
        private MongoConfig CreateConfig(string collection, string database)
        {
            return new MongoDbConfig.MongoConfig()
            {
                ConnectionString = connectionString,
                Database = database,
                Collection = collection,
            };
        }
    }
}
