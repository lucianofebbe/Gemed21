using IP.BaseDomains;
using IP.Repository.Infrastructure.Unit;

namespace IP.Repository.Infrastructure.RepositoryFactory
{
    public interface IRepositoryFactory<T> where T : BaseDomain
    {
        UnitOfWork<T> Create(string connectionString = "");
    }
}
