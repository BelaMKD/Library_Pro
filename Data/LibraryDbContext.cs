using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopies> BookCopies { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Lending> Lendings { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Publisher> Publisers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookPublishers>().HasKey(x => new { x.PublisherId, x.BookId });
        }
    }
}
