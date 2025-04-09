using IP.BaseDomains;
using IP.UnitEntityFramework.Contexts;
using IP.UnitEntityFramework.UnitEntityFramework.Commands;
using IP.UnitEntityFramework.UnitEntityFramework.Request;

namespace IP.UnitEntityFramework.UnitEntityFramework.RepositoryFactory
{
    public interface IRepositoryFactory<T> where T : BaseDomain
    {
        UnitOfWorkCommands<T> CreateCommands(UnitContext context, CancellationToken cancellationToken);
        UnitOfWorkRequest<T> CreateRequests(UnitContext context, CancellationToken cancellationToken);
    }
}
