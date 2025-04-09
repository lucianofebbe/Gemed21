using IP.BaseDomains;
using System.Linq.Expressions;

namespace IP.UnitEntityFramework.UnitEntityFramework.Request
{
    public interface IUnitOfWorkRequest<T>
        where T : BaseDomain
    {
        Task<T> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, string status = "");
        Task<List<T>> GetAll(CancellationToken cancellationToken, string status = "");
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, string status = "");
    }
}