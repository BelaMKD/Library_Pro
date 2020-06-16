using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SqlData
{
    public class BookSqlData : IBookData
    {
        private readonly LibraryDbContext dbContext;

        public BookSqlData(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Book CreateBook(Book book)
        {
            dbContext.Books.Add(book);
            return book;
        }

        public Book DeleteBook(int bookId)
        {
            var tempBook = dbContext.Books
                .SingleOrDefault(x => x.Id == bookId);
            if (tempBook!=null)
            {
                dbContext.Books.Remove(tempBook);
            }
            return tempBook;
        }

        public Book GetBookByid(int bookId)
        {
            return dbContext.Books
                .Include(x => x.BookPublishers)
                .ThenInclude(z => z.Publisher)
                .SingleOrDefault(x => x.Id == bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            return dbContext.Books
                .Include(x=>x.BookPublishers)
                .ThenInclude(z=>z.Publisher)
                .ToList();
        }

        public Book UpdateBook(Book book)
        {
            dbContext.Entry(book).State = EntityState.Modified;
            return book;
        }
    }
}
