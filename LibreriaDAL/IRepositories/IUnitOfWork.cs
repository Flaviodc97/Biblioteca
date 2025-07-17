using BibliotecaDAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository AuthorRepository { get; }
        IGenericRepository<T> GetRepository<T>() where T : class, IEntity;
        Task<int> SaveChangesAsync();
    }
}
