using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAPInsurancesTest.Models;

namespace GAPInsurancesTest.Repositories
{
    public class ClientsRepository : IClientsRepository, IDisposable
    {
        private GAPInsurancesDBEntities db;

        public ClientsRepository(GAPInsurancesDBEntities db)
        {
            this.db = db;
        }

        public void DeleteClient(int client_id)
        {
            Client client = this.db.Clients.Find(client_id);
            this.db.Clients.Remove(client);
        }

        void Dispose()
        {
            throw new NotImplementedException();
        }

        Client IClientsRepository.GetClientById(int client_id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Client> IClientsRepository.GetClients()
        {
            throw new NotImplementedException();
        }

        void IClientsRepository.InsertClient(Client client)
        {
            throw new NotImplementedException();
        }

        void IClientsRepository.Save()
        {
            throw new NotImplementedException();
        }

        void IClientsRepository.UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}