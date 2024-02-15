using IP.BaseDomains;
using IP.Logs.Logs;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Repository.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IP.Repository.Infrastructure.Unit.Request
{
    public class UnitOfWorkRequest<T> : IUnitOfWorkRequest<T>
        where T : BaseDomain
    {
        private readonly Context _context;
        private readonly ILogs _iGenerateLogs;

        public UnitOfWorkRequest(
            Context context,
            ILogsFactory iGenerateLogs)
        {
            try
            {
                _context = context;
                _iGenerateLogs = iGenerateLogs.Create(new LogsSettings() { SendMail = true });
            }
            catch (Exception ex)
            {
                _iGenerateLogs.GenerateLog(ex);
                throw;
            }
        }

        public virtual async Task<List<T>> GetAll(CancellationToken cancellationToken, bool? Deleteds = false)
        {
            try
            {
                if (Deleteds == null)
                    return await _context.Set<T>().ToListAsync(cancellationToken);
                else
                {
                    return await _context.Set<T>()
                        .Where(del => del.Deleted == !(bool)Deleteds)
                        .ToListAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _iGenerateLogs.GenerateLog(ex);
                throw;
            }
        }

        public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            try
            {
                if (Deleteds == null)
                    return await _context.Set<T>()
                        .Where(predicate).ToListAsync(cancellationToken);
                else
                {
                    return await _context.Set<T>()
                        .Where(del => del.Deleted == !(bool)Deleteds)
                        .Where(predicate).ToListAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _iGenerateLogs.GenerateLog(ex);
                throw;
            }
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            try
            {
                if (Deleteds == null)
                    return await _context.Set<T>()
                        .Where(predicate).FirstOrDefaultAsync(cancellationToken);
                else
                {
                    return await _context.Set<T>()
                        .Where(del => del.Deleted == !(bool)Deleteds)
                        .Where(predicate).FirstOrDefaultAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _iGenerateLogs.GenerateLog(ex);
                throw;
            }
        }
    }
}
