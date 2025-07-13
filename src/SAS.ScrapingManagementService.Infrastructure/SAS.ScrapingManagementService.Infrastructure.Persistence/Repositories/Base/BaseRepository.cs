
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SAS.ScrapingManagementService.Infrastructure.Persistence.AppDataContext;
using SAS.SharedKernel.Entities;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Utilities;

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.Repositories.Base
{
    public class BaseRepository<T, TId> : IRepository<T, TId> where T : BaseEntity<TId>
    {
        protected AppDbContext _dbContext;
        internal DbSet<T> dbSet;
        public BaseRepository(AppDbContext context)
        {
            _dbContext = context;

            dbSet = context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            EntityEntry<T> entry = await dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entry.Entity;

        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await dbSet.ToListAsync();
        }
        public async Task<IEnumerable<T>> ListAsync(ISpecification<T> specification)
        {
            var q = ApplySpecification(specification);

            return await q.ToListAsync();
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(TId id, ISpecification<T> specification = null)
        {
            var q = ApplySpecification(specification);
            return await q.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        protected IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T, TId>.GetQuery(dbSet.AsQueryable(), specification);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.BulkInsertAsync(entities.ToList());
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            return await query.FirstOrDefaultAsync();
        }
    }

}
