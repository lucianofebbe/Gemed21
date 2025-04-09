using IP.BaseDomains;
using IP.MongoDb.MongoDbConfig;
using MongoDB.Bson;
using MongoDB.Driver;

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
            try
            {
                var collection = GetCollection().Result;
                var filter = Builders<T>.Filter.Eq("Id", model.Id);
                return await Task.FromResult(collection.DeleteOne(filter, cancellationToken).IsAcknowledged);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<T> Get(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken)
        {
            try
            {
                var collection = GetCollection().Result;
                var documento = collection.Find(filterDefinition).FirstOrDefault(cancellationToken);
                return await Task.FromResult(documento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<T>> GetAll(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken)
        {
            try
            {
                var collection = database.GetCollection<T>(config.Collection);
                var result = new List<T>();

                if (filterDefinition != null)
                    result.AddRange(collection.Find(filterDefinition).ToList(cancellationToken));
                else
                    result.AddRange(collection.Find(FilterDefinition<T>.Empty).ToList());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<string> Insert(T model, CancellationToken cancellationToken)
        {
            try
            {
                var collection = database.GetCollection<T>(config.Collection);
                model.Id = ObjectId.GenerateNewId();
                collection.InsertOne(model);
                return model.Id.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> Update(T model, CancellationToken cancellationToken)
        {
            try
            {
                var collection = database.GetCollection<T>(config.Collection);
                var filter = Builders<T>.Filter.Eq("Id", model.Id);
                return await Task.FromResult(collection.ReplaceOne(filter, model).IsAcknowledged);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private Task<IMongoCollection<T>> GetCollection()
        {
            try
            {
                var collection = database.GetCollection<T>(config.Collection);
                return Task.FromResult(collection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private Task<IMongoDatabase> GetDatabase()
        {
            try
            {
                var client = new MongoClient(config.ConnectionString);
                var database = client.GetDatabase(config.Database);
                return Task.FromResult(database);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
