using IP.BaseDomains;
using IP.Repository.Infrastructure.Unit;

namespace IP.Repository.Infrastructure.Repositories.SegUsuario
{
    public interface ISegUsuarioRepository<T> : IUnitOfWork<T>
        where T : BaseDomain
    {
    }
}
