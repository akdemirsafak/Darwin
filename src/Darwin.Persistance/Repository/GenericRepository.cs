﻿using Darwin.Domain.RepositoryCore;
using Darwin.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Darwin.Persistance.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DarwinDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DarwinDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return await (filter == null ?
                    _dbSet.ToListAsync() :
                    _dbSet.Where(filter).ToListAsync());
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.SingleOrDefaultAsync(filter);
        }

        public async Task RemoveAsync(T entity)
        {
            var deletedEntity = _dbSet.Remove(entity);
            deletedEntity.State = EntityState.Deleted;

        }

        public async Task UpdateAsync(T entity)
        {
            var updatedEntity = _dbSet.Update(entity);
            updatedEntity.State = EntityState.Modified;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var addedEntity = _dbSet.Add(entity);
            addedEntity.State = EntityState.Added;
            return entity;
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            return _dbSet.AsNoTracking();
        }
    }
}
