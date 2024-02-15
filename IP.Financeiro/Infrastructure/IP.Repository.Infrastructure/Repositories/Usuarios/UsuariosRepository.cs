using IP.Domain;
using System.Linq.Expressions;
using IP.Repository.Infrastructure.Unit;

namespace IP.Repository.Infrastructure.Usuarios
{
    public class UsuariosRepository : IUsuarioRepository<Usuario>
    {
        protected readonly IUnitOfWork<Domain.Usuario> unitOfWork;
        public UsuariosRepository(
            IUnitOfWork<Domain.Usuario> unit)
        {
            unitOfWork = unit;
        }

        public Task<Domain.Usuario> Add(Domain.Usuario entidade, CancellationToken cancellationToken)
        {
            return unitOfWork.Add(entidade, cancellationToken);
        }

        public Task<bool> Delete(Domain.Usuario entidade, CancellationToken cancellationToken)
        {
            return unitOfWork.Delete(entidade, cancellationToken);
        }

        public Task<Domain.Usuario> Get(Expression<Func<Domain.Usuario, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.Get(predicate, cancellationToken, Deleteds);
        }

        public Task<List<Domain.Usuario>> GetAll(CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.GetAll(cancellationToken, Deleteds);
        }

        public Task<List<Domain.Usuario>> GetAll(Expression<Func<Domain.Usuario, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            return unitOfWork.GetAll(predicate, cancellationToken, Deleteds);
        }

    }
}
