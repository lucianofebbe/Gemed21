using IP.BaseDomains;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using IP.UnitEntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IP.UnitEntityFramework.UnitEntityFramework.Request
{
    public class UnitOfWorkRequest<T> : IUnitOfWorkRequest<T>
        where T : BaseDomain
    {
        private readonly UnitContext _context;
        private readonly ILogsFactory logsFactory;

        public UnitOfWorkRequest(
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

        public virtual async Task<List<T>> GetAll(CancellationToken cancellationToken, string status = "")
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                    return await _context.Set<T>().ToListAsync(cancellationToken);
                else
                {
                    return await _context.Set<T>()
                        .Where(del => del.Status == status)
                        .ToListAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, string status = "")
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                    return await _context.Set<T>()
                        .Where(predicate).ToListAsync(cancellationToken);
                else
                {
                    return await _context.Set<T>()
                        .Where(del => del.Status == status)
                        .Where(predicate).ToListAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, string status = "")
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                    return await _context.Set<T>()
                        .Where(predicate).FirstOrDefaultAsync(cancellationToken);
                else
                {
                    return await _context.Set<T>()
                        .Where(del => del.Status == status)
                        .Where(predicate).FirstOrDefaultAsync(cancellationToken);
                }
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
