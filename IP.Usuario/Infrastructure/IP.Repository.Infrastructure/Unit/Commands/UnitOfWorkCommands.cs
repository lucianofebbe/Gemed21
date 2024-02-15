using IP.BaseDomains;
using IP.Logs.Logs;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Repository.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace IP.Repository.Infrastructure.Unit.Commands
{
    public class UnitOfWorkCommands<T> : IUnitOfWorkCommands<T>
        where T : BaseDomain
    {
        private readonly Context _context;
        private readonly ILogs _iGenerateLogs;

        public UnitOfWorkCommands(
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

        public virtual async Task<T> Add(T entidade, CancellationToken cancellationToken)
        {
            try
            {
                _context.Entry(entidade).State = entidade.Id == 0 ?
                    EntityState.Added : EntityState.Modified;

                await _context.SaveChangesAsync(cancellationToken);
                return entidade;
            }
            catch (Exception ex)
            {
                _iGenerateLogs.GenerateLog(ex);
                throw;
            }
        }

        public virtual async Task<bool> Delete(T entidade, CancellationToken cancellationToken)
        {
            try
            {
                if (entidade != null)
                {
                    entidade.Deleted = true;
                    return Add(entidade, cancellationToken).Result.Id > 0;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _iGenerateLogs.GenerateLog(ex);
                throw;
            }
        }
    }
}
