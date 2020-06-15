using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SqlData
{
    public class LibrarySqlData : ILibraryData
    {
        private readonly LibraryDbContext dbContext;

        public LibrarySqlData(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Library CreateLibrary(Library library)
        {
            dbContext.Libraries.Add(library);
            return library;
        }

        public Library DeleteLibrary(int libraryId)
        {
            var tempLibrary = dbContext.Libraries
                .SingleOrDefault(x => x.Id == libraryId);
            if (tempLibrary!=null)
            {
                dbContext.Libraries.Remove(tempLibrary);
            }
            return tempLibrary;
        }

        public Library GetLibraryById(int libraryId)
        {
            return dbContext.Libraries
                .Include(x => x.BookCopies)
                .ThenInclude(y => y.Book)
                .SingleOrDefault(x => x.Id == libraryId);
        }

        public IEnumerable<Library> GetLibraries()
        {
            return dbContext.Libraries
                .Include(x => x.BookCopies)
                .ThenInclude(y => y.Book)
                .ToList();
        }

        public Library UpdateLibrary(Library library)
        {
            dbContext.Entry(library).State = EntityState.Modified;
            return library;
        }
    }
}
