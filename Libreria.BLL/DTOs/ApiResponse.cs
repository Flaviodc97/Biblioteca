using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBLL.DTOs
{
    public record ApiResponse <T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; } 
    }
}
