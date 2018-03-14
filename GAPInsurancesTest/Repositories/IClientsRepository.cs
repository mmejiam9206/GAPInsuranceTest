using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPInsurancesTest.Models;

namespace GAPInsurancesTest.Repositories
{
    interface IClientsRepository : IDisposable
    {
        IEnumerable<Client> GetClients();
        Client GetClientById(int client_id);
        void InsertClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(int client_id);
        void Save();
    }
}
