using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.DTOs
{
    public record PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short YearOfPublication { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
    }
}
