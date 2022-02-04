using MotoShop.DL.InMemoryDb;
using MotoShop.DL.Interfaces;
using MotoShop.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MotoShop.DL.Repositories
{
    public class MotoInMemoryRepository : IMotoRepository
    {
        public MotoInMemoryRepository()
        { }

        public Moto Create(Moto moto)
        {
            MotoInMemoryCollection.MotoDb.Add(moto);

            return moto;
        }

        public Moto Delete(int id)
        {
            var moto = MotoInMemoryCollection.MotoDb.FirstOrDefault(moto => moto.Id == id);

            MotoInMemoryCollection.MotoDb.Remove(moto);

            return moto;
        }

        public IEnumerable<Moto> GetAll()
        {
            return MotoInMemoryCollection.MotoDb;
        }

        public Moto GetById(int id)
        {
            return MotoInMemoryCollection.MotoDb.FirstOrDefault(x => x.Id == id);
        }

        public Moto Update(Moto moto)
        {
            var result = MotoInMemoryCollection.MotoDb.FirstOrDefault(x => x.Id == moto.Id);

            result.Brand = moto.Brand;
            result.Model = moto.Model;

            return result;
        }
    }
}
