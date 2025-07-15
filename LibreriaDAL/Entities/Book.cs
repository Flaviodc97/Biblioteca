using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Summary { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public IList<Loan> Loans { get; set; }
        public IList<Review> Reviews { get; set; }
        public IList<Reservation> Reservations { get; set; }
        public IList<Author> Authors { get; set; }
        public IList<Category> Categories { get; set; }
    }
}
