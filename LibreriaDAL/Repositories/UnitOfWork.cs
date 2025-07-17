using BibliotecaDAL.Context;
using BibliotecaDAL.Interface;
using BibliotecaDAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BibliotecaDbContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(BibliotecaDbContext context)
        {
            _context = context;
            AuthorRepository = new AuthorRepository(_context);
            _repositories = new Dictionary<Type, object>();

        }

        public IAuthorRepository AuthorRepository { get; private set; }
        public IGenericRepository<T> GetRepository<T>() where T : class, IEntity
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IGenericRepository<T>)_repositories[typeof(T)];
            }

            var repository = new GenericRepository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
