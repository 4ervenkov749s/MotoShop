using MotoShop.Models.DTO;
using System.Collections.Generic;

namespace MotoShop.BL.Interfaces
{
    public interface IClientService
    {
        Client Create(Client client);
        Client Update(Client client);
        Client Delete(int id);
        Client GetById(int id);
        IEnumerable<Client> GetAll();
    }
}
