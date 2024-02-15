using IP.BaseDomains;

namespace IP.Repository.Infrastructure.Unit.Commands
{
    public interface IUnitOfWorkCommands<T>
        where T : BaseDomain
    {
        Task<T> Add(T entidade, CancellationToken cancellationToken);
        Task<bool> Delete(T entidade, CancellationToken cancellationToken);
    }
}