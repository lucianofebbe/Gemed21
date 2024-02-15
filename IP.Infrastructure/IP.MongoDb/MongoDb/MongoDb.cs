using IP.BaseDomains;
using IP.MongoDb.MongoDbConfig;
using MongoDB.Driver;
using System.Threading;

namespace IP.MongoDb.MongoDb
{
    public class MongoDb<T> : IMongoDb<T>
        where T : BaseDomainMongo
    {
        private readonly MongoConfig config;
        private readonly IMongoDatabase database;

        public MongoDb(MongoConfig config)
        {

            this.config = config;
            database = GetDatabase().Result;
        }

        public async Task<bool> Delete(T model, CancellationToken cancellationToken)
        {
            var collection = GetCollection().Result;
            var filter = Builders<T>.Filter.Eq("Id", model.Id);
            return await Task.FromResult(collection.DeleteOne(filter, cancellationToken).IsAcknowledged);
        }

        public async Task<T> Get(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken)
        {
            var collection = GetCollection().Result;
            var documento = collection.Find(filterDefinition).FirstOrDefault();
            return await Task.FromResult(documento);
        }

        public async Task<List<T>> GetAll(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken)
        {
            var collection = database.GetCollection<T>(config.Collection);
            var result = new List<T>(); ;

            if (filterDefinition != null)
                result.AddRange(collection.Find(filterDefinition).ToList());
            else
                result.AddRange(collection.Find(FilterDefinition<T>.Empty).ToList());
            return result;
        }

        public async Task Insert(T model, CancellationToken cancellationToken)
        {
            var collection = database.GetCollection<T>(config.Collection);
            model.Id = NextItemInCollection(cancellationToken).Result;
            collection.InsertOne(model);
        }

        public async Task<bool> Update(T model, CancellationToken cancellationToken)
        {
            var collection = database.GetCollection<T>(config.Collection);
            var filter = Builders<T>.Filter.Eq("Id", model.Id);
            return await Task.FromResult(collection.ReplaceOne(filter, model).IsAcknowledged);

        }

        private Task<long> NextItemInCollection(CancellationToken cancellationToken)
        {
            var collection = database.GetCollection<T>(config.Collection);
            var count = collection.CountDocuments(FilterDefinition<T>.Empty, null, cancellationToken);
            return Task.FromResult(count + 1);
        }

        private Task<IMongoCollection<T>> GetCollection()
        {
            var collection = database.GetCollection<T>(config.Collection);
            return Task.FromResult(collection);
        }

        private Task<IMongoDatabase> GetDatabase()
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.Database);
            return Task.FromResult(database);
        }
    }
}
