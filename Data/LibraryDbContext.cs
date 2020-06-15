using Core;
using Microsoft.EntityFrameworkCore;
using System;

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
            modelBuilder.Entity<Book>().HasMany(publisher => publisher.Publisers);
            modelBuilder.Entity<Publisher>().HasMany(book => book.Books);

            base.OnModelCreating(modelBuilder);
        }
    }
}
