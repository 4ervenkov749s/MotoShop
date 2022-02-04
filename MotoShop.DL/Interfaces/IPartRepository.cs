using MotoShop.Models.DTO;
using System.Collections.Generic;

namespace MotoShop.DL.Interfaces
{
    public interface IPartRepository
    {
        Part Create(Part part);
        Part Update(Part part);
        Part Delete(int id);
        Part GetById(int id);
        IEnumerable<Part> GetAll();
    }
}
