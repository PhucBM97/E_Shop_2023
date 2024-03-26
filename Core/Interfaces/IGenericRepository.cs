using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetDataWithPredicate(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
    }
}
