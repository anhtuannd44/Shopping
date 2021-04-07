using ShoppingProject.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingProject.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbFactory _dbFactory;
        private DbSet<T> _dbSet;
        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<T>());
        }
        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public virtual IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }
        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }
        public virtual async Task<T> FindByIdAsync(params object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }

        public virtual async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }
        public virtual async Task CreateRangeAsync(List<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }
               
        public virtual void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
        public virtual void RemoveRange(Expression<Func<T, bool>> predicate)
        {
            var entitiesToDelete = FindAll(predicate).ToList();
            DbSet.RemoveRange(entitiesToDelete);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
