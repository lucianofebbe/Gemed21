using IP.BaseDomains;
using IP.Repository.Infrastructure.Unit;

namespace IP.Repository.Infrastructure.UsuarioAcessos
{
    public interface IUsuarioAcessoRepository<T> : IUnitOfWork<T>
        where T : BaseDomain
    {
    }
}
