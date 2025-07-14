using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short YearOfPublication { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public IList<Book> Books { get; set; }

    }
}
