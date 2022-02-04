using MotoShop.BL.Interfaces;
using MotoShop.DL.Interfaces;
using MotoShop.Models.DTO;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace MotoShop.BL.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ILogger _logger;

        public ServiceService(IServiceRepository serviceRepository, ILogger logger)
        {
            _serviceRepository = serviceRepository;
            _logger = logger;
        }

        public Service Create(Service service)
        {
            var index = _serviceRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault()?.Id;
            service.Id = (int)(index != null ? index + 1 : 1);
            return _serviceRepository.Create(service);
        }

        public Service Delete(int id)
        {
            return _serviceRepository.Delete(id);
        }

        public Service GetById(int id)
        {
            return _serviceRepository.GetById(id);
        }

        public IEnumerable<Service> GetAll()
        {
            return _serviceRepository.GetAll();
        }

        public Service Update(Service service)
        {
            return _serviceRepository.Update(service);
        }
    }
}
