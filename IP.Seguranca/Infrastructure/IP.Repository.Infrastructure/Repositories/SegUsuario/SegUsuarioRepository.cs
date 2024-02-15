using IP.Repository.Infrastructure.Unit;
using System.Linq.Expressions;

namespace IP.Repository.Infrastructure.Repositories.SegUsuario
{
    public class SegUsuarioRepository : ISegUsuarioRepository<Domain.SegUsuario>
    {
        protected readonly IUnitOfWork<Domain.SegUsuario> unitOfWork;

        public SegUsuarioRepository(IUnitOfWork<Domain.SegUsuario> unit)
        {
            unitOfWork = unit;
        }

        public Task<Domain.SegUsuario> Add(Domain.SegUsuario entidade, CancellationToken cancellationToken)
        {
            return unitOfWork.Add(entidade, cancellationToken);
        }

        public Task<bool> Delete(Domain.SegUsuario entidade, CancellationToken cancellationToken)
        {
            return unitOfWork.Delete(entidade, cancellationToken);
        }

        public Task<Domain.SegUsuario> Get(Expression<Func<Domain.SegUsuario, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.Get(predicate, cancellationToken, Deleteds);
        }

        public Task<List<Domain.SegUsuario>> GetAll(CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.GetAll(cancellationToken, Deleteds);
        }

        public Task<List<Domain.SegUsuario>> GetAll(Expression<Func<Domain.SegUsuario, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.GetAll(predicate, cancellationToken, Deleteds);
        }
    }
}
