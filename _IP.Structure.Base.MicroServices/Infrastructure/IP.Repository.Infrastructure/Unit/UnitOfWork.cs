using IP.BaseDomains;
using IP.Logs.GeneratorFactory;
using IP.Logs.GeneratorLog;
using IP.Logs.GeneratorLogSettings;
using IP.Repository.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IP.Repository.Infrastructure.Unit
{
    public class UnitOfWork<T> : IUnitOfWork<T>
        where T : BaseDomain
    {
        private Context _context;
        private IGeneratorLog _iGenerateLogs;

        public UnitOfWork(
            Context context,
            GeneratorLogFactory iGenerateLogs)
        {
            try
            {
                _context = context;
                _iGenerateLogs = iGenerateLogs.Create(new LogSettings() { SendMail = true });
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

        public virtual Task<List<T>> GetAll(CancellationToken cancellationToken, bool? Deleteds = false)
        {
            try
            {
                if (Deleteds == null)
                    return _context.Set<T>().ToListAsync(cancellationToken);
                else
                {
                    return _context.Set<T>()
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

        public virtual Task<List<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            try
            {
                if (Deleteds == null)
                    return _context.Set<T>()
                        .Where(predicate).ToListAsync(cancellationToken);
                else
                {
                    return _context.Set<T>()
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

        public virtual Task<T> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool? Deleteds = false)
        {
            try
            {
                if (Deleteds == null)
                    return _context.Set<T>()
                        .Where(predicate).FirstOrDefaultAsync(cancellationToken);
                else
                {
                    return _context.Set<T>()
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
