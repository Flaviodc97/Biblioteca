using BibliotecaDAL.Context;
using BibliotecaDAL.Interface;
using BibliotecaDAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly BibliotecaDbContext _context;
        private protected DbSet<T> _table;
        public GenericRepository(BibliotecaDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _table.AddAsync(entity);
            return result.State == EntityState.Added ? entity : null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _table.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<List<T>> GetByIdRangeAsync(List<int> ids)
        {
            var result = await _table.Where(x => ids.Contains(x.Id)).ToListAsync();
            return result;
        }

        public async Task<T?> GetByIdAsync(params object[] id)
        {
            var result = await  _table.FindAsync(id);
            return result;
        } 

        public bool Remove(T entity)
        {
            var result = _table.Remove(entity);
            return result.State == EntityState.Deleted;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result =  _table.Update(entity);
            return result.State == EntityState.Modified ? entity : null;
        }

        public  IQueryable<T> GetQueryable()
        {
            var result =  _table.AsNoTracking().AsQueryable();
            return result;
        }
    }
}
