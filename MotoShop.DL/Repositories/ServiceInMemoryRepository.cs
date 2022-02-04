using MotoShop.DL.InMemoryDb;
using MotoShop.DL.Interfaces;
using MotoShop.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MotoShop.DL.Repositories
{
    public class ServiceInMemoryRepository : IServiceRepository
    {
        public ServiceInMemoryRepository()
        { }

        public Service Create(Service service)
        {
            ServiceInMemoryCollection.ServiceDb.Add(service);

            return service;
        }

        public Service Delete(int id)
        {
            var service = ServiceInMemoryCollection.ServiceDb.FirstOrDefault(service => service.Id == id);

            ServiceInMemoryCollection.ServiceDb.Remove(service);

            return service;
        }

        public IEnumerable<Service> GetAll()
        {
            return ServiceInMemoryCollection.ServiceDb;
        }

        public Service GetById(int id)
        {
            return ServiceInMemoryCollection.ServiceDb.FirstOrDefault(x => x.Id == id);
        }

        public Service Update(Service service)
        {
            var result = ServiceInMemoryCollection.ServiceDb.FirstOrDefault(x => x.Id == service.Id);

            result.Name = service.Name;

            return result;
        }
    }
}
