using MotoShop.Models.DTO;
using System.Collections.Generic;

namespace MotoShop.DL.Interfaces
{
    public interface IMotoRepository
    {
        Moto Create(Moto moto);
        Moto Update(Moto moto);
        Moto Delete(int id);
        Moto GetById(int id);
        IEnumerable<Moto> GetAll();

    }
}
