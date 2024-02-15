using IP.Domain;
using IP.Repository.Infrastructure.Unit;
using System.Linq.Expressions;

namespace IP.Repository.Infrastructure.UsuarioAcessos
{
    public class UsuarioAcessoRepository : IUsuarioAcessoRepository<UsuarioAcesso>
    {
        protected readonly IUnitOfWork<UsuarioAcesso> unitOfWork;

        public UsuarioAcessoRepository(IUnitOfWork<UsuarioAcesso> unit)
        {
            unitOfWork = unit;
        }

        public Task<UsuarioAcesso> Add(UsuarioAcesso entidade, CancellationToken cancellationToken)
        {
            return unitOfWork.Add(entidade, cancellationToken);
        }

        public Task<bool> Delete(UsuarioAcesso entidade, CancellationToken cancellationToken)
        {
            return unitOfWork.Delete(entidade, cancellationToken);
        }
        public Task<UsuarioAcesso> Get(Expression<Func<UsuarioAcesso, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.Get(predicate, cancellationToken, Deleteds);
        }

        public Task<List<UsuarioAcesso>> GetAll(CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.GetAll(cancellationToken, Deleteds);
        }

        public Task<List<UsuarioAcesso>> GetAll(Expression<Func<UsuarioAcesso, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.GetAll(predicate, cancellationToken, Deleteds);
        }

    }
}
