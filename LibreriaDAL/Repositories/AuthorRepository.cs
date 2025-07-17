using BibliotecaDAL.Context;
using BibliotecaDAL.Entities;
using BibliotecaDAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BibliotecaDbContext context) : base(context)
        {
            
        }

        public async Task<Author> GetAuthorWithBooks(int id)
        {
            var result = await _table.Include(x => x.Books)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }
    }
}
