using IP.BaseDomains;

namespace IP.UnitEntityFramework.UnitEntityFramework.Commands
{
    public interface IUnitOfWorkCommands<T>
        where T : BaseDomain
    {
        Task<T> Insert(T entidade, CancellationToken cancellationToken);
        Task<T> Update(T entidade, CancellationToken cancellationToken);
        Task<bool> Delete(T entidade, CancellationToken cancellationToken);
    }
}