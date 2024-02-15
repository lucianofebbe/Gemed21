using IP.Repository.Infrastructure.Unit;
using System.Linq.Expressions;

namespace IP.Repository.Infrastructure.Repositories.SegEmpresa
{
    public class SegEmpresaRepository : ISegEmpresaRepository<Domain.SegEmpresa>
    {
        protected readonly IUnitOfWork<Domain.SegEmpresa> unitOfWork;

        public SegEmpresaRepository(IUnitOfWork<Domain.SegEmpresa> unit)
        {
            unitOfWork = unit;
        }

        public Task<Domain.SegEmpresa> Add(Domain.SegEmpresa entidade, CancellationToken cancellationToken)
        {
            return unitOfWork.Add(entidade, cancellationToken);
        }

        public Task<bool> Delete(Domain.SegEmpresa entidade, CancellationToken cancellationToken)
        {
            return unitOfWork.Delete(entidade, cancellationToken);
        }

        public Task<Domain.SegEmpresa> Get(Expression<Func<Domain.SegEmpresa, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.Get(predicate, cancellationToken, Deleteds);
        }

        public Task<List<Domain.SegEmpresa>> GetAll(CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.GetAll(cancellationToken, Deleteds);
        }

        public Task<List<Domain.SegEmpresa>> GetAll(Expression<Func<Domain.SegEmpresa, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.GetAll(predicate, cancellationToken, Deleteds);
        }
    }
}
