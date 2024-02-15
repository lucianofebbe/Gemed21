using IP.BaseDomains;
using IP.Repository.Infrastructure.Unit;

namespace IP.Repository.Infrastructure.Usuarios
{
    public interface IUsuarioRepository<T> : IUnitOfWork<T>
        where T : BaseDomain
    {
    }
}
