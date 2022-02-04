using MotoShop.BL.Interfaces;
using MotoShop.DL.Interfaces;
using MotoShop.Models.DTO;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace MotoShop.BL.Services
{
    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _motoRepository;
        private readonly ILogger _logger;

        public MotoService(IMotoRepository motoRepository, ILogger logger)
        {
            _motoRepository = motoRepository;
            _logger = logger;
        }

        public Moto Create(Moto moto)
        {
            var index = _motoRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault()?.Id;
            moto.Id = (int)(index != null ? index + 1 : 1);
            return _motoRepository.Create(moto);
        }

        public Moto Delete(int id)
        {
            return _motoRepository.Delete(id);
        }

        public Moto GetById(int id)
        {
            return _motoRepository.GetById(id);
        }

        public IEnumerable<Moto> GetAll()
        {
            return _motoRepository.GetAll();
        }

        public Moto Update(Moto moto)
        {
            return _motoRepository.Update(moto);
        }
    }
}
