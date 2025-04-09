using IP.BaseDomains;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using IP.UnitEntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace IP.UnitEntityFramework.UnitEntityFramework.Commands
{
    public class UnitOfWorkCommands<T> : IUnitOfWorkCommands<T>
        where T : BaseDomain
    {
        private readonly UnitContext _context;
        private readonly ILogsFactory logsFactory;

        public UnitOfWorkCommands(
            UnitContext context,
            ILogsFactory logsFactory)
        {
            try
            {
                _context = context;
                this.logsFactory = logsFactory;
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        public virtual async Task<T> Insert(T entidade, CancellationToken cancellationToken)
        {
            try
            {
                _context.Entry(entidade).State = EntityState.Added;
                await _context.SaveChangesAsync(cancellationToken);
                return entidade;
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        public virtual async Task<T> Update(T entidade, CancellationToken cancellationToken)
        {
            try
            {
                _context.Entry(entidade).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);
                return entidade;
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        public virtual async Task<bool> Delete(T entidade, CancellationToken cancellationToken)
        {
            try
            {
                if (entidade != null)
                {
                    entidade.Status = "D";
                    return Update(entidade, cancellationToken).Result != null;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
