using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Data.SqlData
{
    public class LendingSqlData : ILendingData
    {
        private readonly LibraryDbContext dbContext;

        public LendingSqlData(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Lending CreateLending(Lending lending)
        {
            dbContext.Lendings
                .Add(lending);
            return lending;
        }

        public Lending DeleteLenging(int lendingId)
        {
            var lending = dbContext.Lendings
                .SingleOrDefault(x => x.Id == lendingId);
            if (lending!=null)
            {
                dbContext.Lendings.Remove(lending);
            }
            return lending;
        }

        public Lending GetLendingById(int lendingId)
        {
            return dbContext.Lendings
                .SingleOrDefault(x => x.Id == lendingId);
        }

        public IEnumerable<Lending> GetLendings()
        {
            return dbContext.Lendings
                .ToList();
        }

        public IEnumerable<Lending> GetLendingsNotReturned(int libraryId)
        {
            var tempLending = dbContext.Lendings
                .Include(x=>x.Client)
                .Include(x => x.Book)
                .ThenInclude(y => y.BookCopies)
                .ThenInclude(z => z.Library)
                .Where(x => x.Book.BookCopies.LibraryId == libraryId);
            return tempLending.Where(x => x.DatumVratena == null).ToList();
        }

        public IEnumerable<Lending> GetLendingsReturned(int libraryId)
        {
            var tempLending = dbContext.Lendings
                .Include(x => x.Client)
                .Include(x => x.Book)
                .ThenInclude(y => y.BookCopies)
                .ThenInclude(z => z.Library)
                .Where(x => x.Book.BookCopies.LibraryId == libraryId);
            return tempLending.Where(x => x.DatumVratena != null).ToList();
        }

        public Lending UpdateLending(Lending lending)
        {
            dbContext.Entry(lending).State = EntityState.Modified;
            return lending;
        }
    }
}
