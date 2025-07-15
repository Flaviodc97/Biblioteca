using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte Valutation { get; set; }
        public string ContentPath { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
