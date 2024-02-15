
using IP.BaseDomains;
using System.Linq.Expressions;

namespace IP.Repository.Infrastructure.Unit
{
    public interface IUnitOfWork<T> where T : BaseDomain
    {
        Task<T> Add(T entidade, CancellationToken cancellationToken);
        Task<bool> Delete(T entidade, CancellationToken cancellationToken);
        Task<T> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false);
        Task<List<T>> GetAll(CancellationToken cancellationToken, bool? Deleteds = false);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false);
    }
}