using IP.BaseDomains;
using IP.DomainMongo;
using IP.Logs.LogsFactory;
using IP.MongoDb.MongoDb;
using IP.MongoDb.MongoDbFactory;
using IP.Repository.Infrastructure.Contexts;
using IP.Repository.Infrastructure.Unit.Commands;
using IP.Repository.Infrastructure.Unit.Request;
using MongoDB.Driver;

namespace IP.Repository.Infrastructure.RepositoryFactory
{
    public class RepositoryFactory<T> : IRepositoryFactory<T> where T : BaseDomain
    {
        private readonly MongoDb<TipoEmpresa> iMongoDbFactory;

        public RepositoryFactory(IMongoDbFactory<TipoEmpresa> iMongoDbFactory)
        {
            this.iMongoDbFactory = iMongoDbFactory.Create("TipoEmpresas");
        }

        public UnitOfWorkCommands<T> CreateCommands(CancellationToken cancellationToken, string connectionStringName = "")
        {
            var context = new Context(GetConnectionString(cancellationToken, connectionStringName));
            var logFactory = new LogsFactory();
            return new UnitOfWorkCommands<T>(context, logFactory);
        }

        public UnitOfWorkRequest<T> CreateGets(CancellationToken cancellationToken, string connectionStringName = "")
        {
            var context = new Context(GetConnectionString(cancellationToken, connectionStringName));
            var logFactory = new LogsFactory();
            return new UnitOfWorkRequest<T>(context, logFactory);
        }

        private string GetConnectionString(CancellationToken cancellationToken, string connectionStringName)
        {
            string cString = string.Empty;
            if (!string.IsNullOrEmpty(connectionStringName))
                cString = iMongoDbFactory.Get(Builders<TipoEmpresa>.Filter.Where(o => o.Type == connectionStringName), cancellationToken).Result.ConnectionString;
            else
                cString = iMongoDbFactory.Get(Builders<TipoEmpresa>.Filter.Where(o => o.Type == "GemedDefault"), cancellationToken).Result.ConnectionString;
            return cString;
        }
    }
}
