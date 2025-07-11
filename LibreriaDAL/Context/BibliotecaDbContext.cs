using BibliotecaDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL.Context
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
        { 
        
        }

        public DbSet<User> User { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Category> Category { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Notifications)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Publisher>()
                .HasMany(e => e.Books)
                .WithOne(e => e.Publisher)
                .HasForeignKey(e => e.PublisherId);

        }
    }
}
