using IP.BaseDomains;
using IP.Logs.GeneratorFactory;
using IP.Repository.Infrastructure.Contexts;
using IP.Repository.Infrastructure.Unit;

namespace IP.Repository.Infrastructure.RepositoryFactory
{
    public class RepositoryFactory<T> : IRepositoryFactory<T> where T : BaseDomain
    {
        public UnitOfWork<T> Create(string connectionString = "")
        {
            var context = new Context(connectionString);
            var logFactory = new GeneratorLogFactory();
            return new UnitOfWork<T>(context, logFactory);
        }
    }
}
