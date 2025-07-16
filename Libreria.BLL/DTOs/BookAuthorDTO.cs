using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.DTOs
{
    public class BookAuthorDTO
    {
        
        public int AuthorId { get; set; }
        public List<int> BookIds { get; set; }

    }
}
