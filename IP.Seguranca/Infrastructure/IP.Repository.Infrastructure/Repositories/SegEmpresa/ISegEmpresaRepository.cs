using IP.BaseDomains;
using IP.Repository.Infrastructure.Unit;

namespace IP.Repository.Infrastructure.Repositories.SegEmpresa
{
    public interface ISegEmpresaRepository<T> : IUnitOfWork<T>
        where T : BaseDomain
    {
    }
}
