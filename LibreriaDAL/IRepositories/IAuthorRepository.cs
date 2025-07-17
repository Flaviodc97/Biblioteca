using BibliotecaDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.IRepositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author> GetAuthorWithBooks(int id);
        Task<(List<Author>, int)> GetAuthorPaginatedAsync(int pageIndex, int pageSize);
    }
}
