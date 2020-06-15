using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SqlData
{
    public class ClientSqlData : IClientData
    {
        private readonly LibraryDbContext dbContext;

        public ClientSqlData(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Client CreateClient(Client client)
        {
            dbContext.Clients.Add(client);
            return client;
        }

        public Client DeleteClient(int clientId)
        {
            var tempClient = dbContext.Clients.SingleOrDefault(x => x.Id == clientId);
            if (tempClient != null)
            {
                dbContext.Clients.Remove(tempClient);
            }
            return tempClient;
        }

        public Client GetClientById(int clientId)
        {
            return dbContext.Clients.SingleOrDefault(x => x.Id == clientId);
        }

        public IEnumerable<Client> GetClients()
        {
            return dbContext.Clients
                .ToList();
        }

        public Client UpdateClient(Client client)
        {
            dbContext.Entry(client).State = EntityState.Modified;
            return client;
        }
    }
}
