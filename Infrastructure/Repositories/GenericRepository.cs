using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly E_ShopContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(E_ShopContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable().AsNoTracking();
            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetDataWithPredicate(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.Where(where).AsNoTracking();
            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                var ent = await _dbSet.AddAsync(entity);
                return ent.Entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Update(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
                _dbSet.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
