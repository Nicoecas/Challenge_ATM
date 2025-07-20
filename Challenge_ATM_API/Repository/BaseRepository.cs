using Challenge_ATM_API.Data;
using Challenge_ATM_API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Challenge_ATM_API.Repository
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default, bool noTracking = true);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        bool ExistId(int id);
        Task<T> AddOrUpdateAsync(T entity, CancellationToken cancellationToken = default);
        DbSet<T> GetSet();
    }

    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly Challenge_ATM_DbContext _dbContext;

        public EfRepository(Challenge_ATM_DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DbSet<T> GetSet() => _dbContext.Set<T>();
        public virtual async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await _dbContext.Set<T>().FindAsync(keyValues, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default, bool noTracking = true)
        {
            if (noTracking)
            {
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
            }
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> AddOrUpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity.Id == 0)
            {
                return await AddAsync(entity, CancellationToken.None);
            }
            else
            {
                await UpdateAsync(entity, CancellationToken.None);
                return entity;
            }
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public bool ExistId(int id)
        {
            return _dbContext.Set<T>().Any(e => e.Id == id);
        }
    }
}
