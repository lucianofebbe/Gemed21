using IP.BaseDomains;
using IP.Logs;
using IP.Mails;
using IP.Repository.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IP.Repository.Infrastructure.Unit
{
    public class UnitOfWork<T> :
        IUnitOfWork<T>
        where T : BaseDomain
    {
        public Context context;
        private IGenerateLogs iGenerateLogs;
        private IGenerateLogsConfig iGenerateLogsConfig;
        private ISendMails iSendMails;

        public UnitOfWork(
            IGenerateLogs iGenerateLogs,
            IGenerateLogsConfig iGenerateLogsConfig,
            ISendMails iSendMails)
        {
            try
            {
                this.iGenerateLogs = iGenerateLogs;
                this.iGenerateLogsConfig = iGenerateLogsConfig;
                this.iSendMails = iSendMails;
                this.context = new Context();
            }
            catch (Exception ex)
            {
                this.iGenerateLogs.GenerateLog(Task.FromResult(ex));
                throw;
            }
        }

        public UnitOfWork(Context context)
        {
            try
            {
                var generateLogsConfig = new GenerateLogsConfig();
                this.iGenerateLogs = new GenerateLogs(generateLogsConfig);
                this.iGenerateLogsConfig = new GenerateLogsConfig();
                this.iSendMails = new SendMails();
                this.context = context;
            }
            catch (Exception ex)
            {
                this.iGenerateLogs.GenerateLog(Task.FromResult(ex));
                throw;
            }
        }

        public virtual async Task<T> Add(T entidade, CancellationToken cancellationToken)
        {
            try
            {
                context.Entry(entidade).State = entidade.Id == 0 ?
                    EntityState.Added : EntityState.Modified;

                await context.SaveChangesAsync(cancellationToken);
                return entidade;
            }
            catch (Exception ex)
            {
                this.iGenerateLogs.GenerateLog(Task.FromResult(ex));
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
                this.iGenerateLogs.GenerateLog(Task.FromResult(ex));
                throw;
            }
        }

        public virtual Task<List<T>> GetAll(CancellationToken cancellationToken, bool? Deleteds = false)
        {
            try
            {
                if (Deleteds == null)
                    return context.Set<T>().ToListAsync(cancellationToken);
                else
                {
                    return context.Set<T>()
                        .Where(del => del.Deleted == !(bool)Deleteds)
                        .ToListAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                this.iGenerateLogs.GenerateLog(Task.FromResult(ex));
                throw;
            }
        }

        public virtual Task<List<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            try
            {
                if (Deleteds == null)
                    return context.Set<T>()
                        .Where(predicate).ToListAsync(cancellationToken);
                else
                {
                    return context.Set<T>()
                        .Where(del => del.Deleted == !(bool)Deleteds)
                        .Where(predicate).ToListAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                this.iGenerateLogs.GenerateLog(Task.FromResult(ex));
                throw;
            }
        }

        public virtual Task<T> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            try
            {
                if (Deleteds == null)
                    return context.Set<T>()
                        .Where(predicate).FirstOrDefaultAsync(cancellationToken);
                else
                {
                    return context.Set<T>()
                        .Where(del => del.Deleted == !(bool)Deleteds)
                        .Where(predicate).FirstOrDefaultAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                this.iGenerateLogs.GenerateLog(Task.FromResult(ex));
                throw;
            }
        }
    }
}
