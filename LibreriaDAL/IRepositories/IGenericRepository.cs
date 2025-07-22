using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetByIdAsync(params object[] id);
        Task<List<T>> GetByIdRangeAsync(List<int> ids);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        bool Remove(T entity);
        IQueryable<T> GetQueryable();
    }
}
