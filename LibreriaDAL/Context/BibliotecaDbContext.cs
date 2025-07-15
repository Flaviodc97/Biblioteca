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
        public DbSet<Loan> Loan { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Reservation> Reservation { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User 1 to n Notification
            modelBuilder.Entity<User>()
                .HasMany(e => e.Notifications)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserID);

            // Publisher 1 to n Book
            modelBuilder.Entity<Publisher>()
                .HasMany(e => e.Books)
                .WithOne(e => e.Publisher)
                .HasForeignKey(e => e.PublisherId);

            // Book 1 to n Loan
            modelBuilder.Entity<Loan>()
                .HasOne(u => u.Book)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.BookId);

            // User 1 to n Loan
            modelBuilder.Entity<Loan>()
                .HasOne(u => u.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.UserId);

            // User 1 to n Review
            modelBuilder.Entity<Review>()
                .HasOne(u => u.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(u => u.UserId);

            // Book 1 to n Review
            modelBuilder.Entity<Review>()
                .HasOne(u => u.Book)
                .WithMany(u => u.Reviews)
                .HasForeignKey(u => u.BookId);

            // Book 1 to n Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(u => u.Book)
                .WithMany(u => u.Reservations)
                .HasForeignKey(u => u.BookId);

            // User 1 to n Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(u => u.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(u => u.UserId);



        }
    }
}
