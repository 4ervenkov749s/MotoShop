using MotoShop.Models.DTO;
using System.Collections.Generic;

namespace MotoShop.DL.Interfaces
{
    public interface IClientRepository
    {
        Client Create(Client client);
        Client Update(Client client);
        Client Delete(int id);
        Client GetById(int id);
        IEnumerable<Client> GetAll();
    }
}
