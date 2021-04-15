using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingProject.Data.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task CreateRangeAsync(List<T> entities);
        Task<T> FindByIdAsync(params object[] keys);
        void Remove(T entity);
        void RemoveRange(Expression<Func<T, bool>> predicate = null);
        void Update(T entity);
        void UpdateRange(List<T> entity);
    }
}