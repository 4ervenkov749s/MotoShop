using MotoShop.BL.Interfaces;
using MotoShop.DL.Interfaces;
using MotoShop.Models.DTO;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace MotoShop.BL.Services
{
    public class PartService : IPartService
    {
        private readonly IPartRepository _partRepository;
        private readonly ILogger _logger;

        public PartService(IPartRepository partRepository, ILogger logger)
        {
            _partRepository = partRepository;
            _logger = logger;
        }

        public Part Create(Part part)
        {
            var index = _partRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault()?.Id;
            part.Id = (int)(index != null ? index + 1 : 1);
            return _partRepository.Create(part);
        }

        public Part Delete(int id)
        {
            return _partRepository.Delete(id);
        }

        public Part GetById(int id)
        {
            return _partRepository.GetById(id);
        }

        public IEnumerable<Part> GetAll()
        {
            return _partRepository.GetAll();
        }

        public Part Update(Part part)
        {
            return _partRepository.Update(part);
        }
    }
}
