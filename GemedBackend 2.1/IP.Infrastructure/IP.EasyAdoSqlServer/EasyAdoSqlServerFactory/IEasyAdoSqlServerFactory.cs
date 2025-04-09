using IP._EasyAdoSqlServer.EasyAdoSqlServer;
using IP._EasyAdoSqlServer.EasyAdoSqlServerConfig;
using IP.BaseDomains;

namespace IP._EasyAdoSqlServer.EasyAdoSqlServerFactory
{
    public interface IEasyAdoSqlServerFactory<T> where T : BaseDomain
    {
        EasyAdoSqlServer<T> Create(EasyAdoSqlServerSettings config);
    }
}
