using IP._EasyAdoSqlServer.EasyAdoSqlServer;
using IP._EasyAdoSqlServer.EasyAdoSqlServerConfig;
using IP.BaseDomains;

namespace IP._EasyAdoSqlServer.EasyAdoSqlServerFactory
{
    public class EasyAdoSqlServerFactory<T> : IEasyAdoSqlServerFactory<T>
         where T : BaseDomain
    {
        public EasyAdoSqlServer<T> Create(EasyAdoSqlServerSettings config)
        {
            return new EasyAdoSqlServer<T>(config);
        }
    }
}
