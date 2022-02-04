using MotoShop.Models.DTO;
using System.Collections.Generic;

namespace MotoShop.DL.Interfaces
{
    public interface IServiceRepository
    {
        Service Create(Service service);
        Service Update(Service service);
        Service Delete(int id);
        Service GetById(int id);
        IEnumerable<Service> GetAll();
    }
}
