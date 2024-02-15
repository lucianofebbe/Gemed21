using IP.BaseDomains;
using IP.Repository.Infrastructure.Unit.Commands;
using IP.Repository.Infrastructure.Unit.Request;

namespace IP.Repository.Infrastructure.RepositoryFactory
{
    public interface IRepositoryFactory<T> where T : BaseDomain
    {
        UnitOfWorkCommands<T> CreateCommands(CancellationToken cancellationToken, string connectionString = "");
        UnitOfWorkRequest<T> CreateGets(CancellationToken cancellationToken, string connectionString = "");
    }
}
