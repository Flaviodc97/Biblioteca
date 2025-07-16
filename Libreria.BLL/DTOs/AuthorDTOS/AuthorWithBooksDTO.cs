using BibliotecaDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.DTOs.AuthorDTOS
{
    public record AuthorWithBooksDTO
    (
        int Id,
        string Name,
        string LastName,
        DateTime DateOfBirth,
        DateTime? DateOfDeath,
        string Biography,
        string Nationality,
        List<BookDTO> Books
    );
}
