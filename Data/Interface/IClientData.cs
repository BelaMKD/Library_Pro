using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IClientData
    {
        Client CreateClient(Client client);
        Client UpdateClient(Client client);
        Client DeleteClient(int clientId);
        Client GetClientById(int clientId);
        IEnumerable<Client> GetClients();
        int Commit();
    }
}
