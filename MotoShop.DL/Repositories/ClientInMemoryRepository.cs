using MotoShop.DL.InMemoryDb;
using MotoShop.DL.Interfaces;
using MotoShop.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MotoShop.DL.Repositories
{
    public class ClientInMemoryRepository : IClientRepository
    {
        public ClientInMemoryRepository()
        { }

        public Client Create(Client client)
        {
            ClientInMemoryCollection.ClientDb.Add(client);

            return client;
        }

        public Client Delete(int id)
        {
            var client = ClientInMemoryCollection.ClientDb.FirstOrDefault(client => client.Id == id);

            ClientInMemoryCollection.ClientDb.Remove(client);

            return client;
        }

        public IEnumerable<Client> GetAll()
        {
            return ClientInMemoryCollection.ClientDb;
        }

        public Client GetById(int id)
        {
            return ClientInMemoryCollection.ClientDb.FirstOrDefault(x => x.Id == id);
        }

        public Client Update(Client client)
        {
            var result = ClientInMemoryCollection.ClientDb.FirstOrDefault(x => x.Id == client.Id);

            result.Name = client.Name;

            return result;
        }
    }
}
