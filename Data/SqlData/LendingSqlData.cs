using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Lending UpdateLending(Lending lending)
        {
            dbContext.Entry(lending).State = EntityState.Modified;
            return lending;
        }
    }
}
