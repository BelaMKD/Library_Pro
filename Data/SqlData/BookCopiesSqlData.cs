using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SqlData
{
    public class BookCopiesSqlData : IBookCopiesData
    {
        private readonly LibraryDbContext dbContext;

        public BookCopiesSqlData(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<BookCopies> BookCopies()
        {
            return dbContext.BookCopies
                .ToList();
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public BookCopies CreateBookCopies(BookCopies bookCopies)
        {
            dbContext.BookCopies.Add(bookCopies);
            return bookCopies;
        }

        public BookCopies DeleteBookCopies(int bookCopiesId)
        {
            var bookCopies = dbContext.BookCopies.SingleOrDefault(x => x.Id == bookCopiesId);
            if (bookCopies != null)
            {
                dbContext.BookCopies.Remove(bookCopies);
            }
            return bookCopies;
        }

        public BookCopies GetBookCopiesById(int bookCopiesId)
        {
            return dbContext.BookCopies
                .Include(x=>x.Book)
                .ThenInclude(y=>y.BookPublishers)
                .Include(x=>x.Library)
                .SingleOrDefault(x => x.Id == bookCopiesId);
        }

        public BookCopies UpdateBookCopies(BookCopies bookCopies)
        {
            dbContext.Entry(bookCopies).State = EntityState.Modified;
            return bookCopies;
        }
    }
}
