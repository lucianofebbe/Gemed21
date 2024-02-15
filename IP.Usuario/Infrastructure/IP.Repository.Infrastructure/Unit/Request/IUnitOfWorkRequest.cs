using IP.BaseDomains;
using System.Linq.Expressions;

namespace IP.Repository.Infrastructure.Unit.Request
{
    public interface IUnitOfWorkRequest<T>
        where T : BaseDomain
    {
        Task<T> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false);
        Task<List<T>> GetAll(CancellationToken cancellationToken, bool? Deleteds = false);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false);
    }
}