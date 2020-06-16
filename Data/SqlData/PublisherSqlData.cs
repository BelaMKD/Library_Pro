using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SqlData
{
    public class PublisherSqlData : IPublisherData
    {
        private readonly LibraryDbContext dbContext;

        public PublisherSqlData(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Publisher CreatePublisher(Publisher publiser)
        {
            dbContext.Publisers.Add(publiser);
            return publiser;
        }

        public Publisher DeletePublisher(int publisherId)
        {
            var tempPublisher = dbContext.Publisers.SingleOrDefault(x => x.Id == publisherId);
            if (tempPublisher!=null)
            {
                dbContext.Publisers.Remove(tempPublisher);
            }
            return tempPublisher;
        }

        public Publisher GetPubliserById(int publisherId)
        {
            return dbContext.Publisers
                .SingleOrDefault(x => x.Id == publisherId);
        }

        public IEnumerable<Publisher> GetPublisers()
        {
            return dbContext.Publisers
                .Include(x=>x.BookPublishers)
                .ThenInclude(z=>z.Book)
                .ToList();
        }

        public Publisher UpdatePublisher(Publisher publiser)
        {
            dbContext.Entry(publiser).State = EntityState.Modified;
            return publiser;
        }
    }
}
