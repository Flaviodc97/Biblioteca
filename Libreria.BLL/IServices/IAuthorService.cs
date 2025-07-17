using BibliotecaBLL.DTOs;
using BibliotecaBLL.DTOs.AuthorDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.IServices
{
    public interface IAuthorService : IService<AuthorDTO>
    {
        Task<AuthorWithBooksDTO> AddBookToAuthorAsync(BookAuthorDTO dto);
        Task<AuthorWithBooksDTO> RemoveBookToAuthorAsync(BookAuthorDTO dto);
        Task<AuthorWithBooksDTO> GetAuthorWithBooks(int id);
        Task<PaginatedListDTO<AuthorDTO>> GetPaginatedListAsync(int pageIndex, int pageSize);
    }
}
