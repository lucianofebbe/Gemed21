using IP.BaseDomains;
using IP.Logs.LogsFactory;
using IP.UnitEntityFramework.Contexts;
using IP.UnitEntityFramework.UnitEntityFramework.Commands;
using IP.UnitEntityFramework.UnitEntityFramework.Request;

namespace IP.UnitEntityFramework.UnitEntityFramework.RepositoryFactory
{
    public class RepositoryFactory<T> : IRepositoryFactory<T> where T : BaseDomain
    {
        public UnitOfWorkCommands<T> CreateCommands(UnitContext context, CancellationToken cancellationToken)
        {
            var logFactory = new LogsFactory();
            return new UnitOfWorkCommands<T>(context, logFactory);
        }

        public UnitOfWorkRequest<T> CreateRequests(UnitContext context, CancellationToken cancellationToken)
        {
            var logFactory = new LogsFactory();
            return new UnitOfWorkRequest<T>(context, logFactory);
        }
    }
}
