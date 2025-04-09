using IP.BaseDomains;
using MongoDB.Driver;

namespace IP.MongoDb.MongoDb
{
    public interface IMongoDb<T> where T : BaseDomainMongo
    {
        Task<string> Insert(T model, CancellationToken cancellationToken);
        Task<bool> Update(T model, CancellationToken cancellationToken);
        Task<bool> Delete(T model, CancellationToken cancellationToken);
        Task<T> Get(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken);
        Task<List<T>> GetAll(FilterDefinition<T> filterDefinition, CancellationToken cancellationToken);
    }
}
