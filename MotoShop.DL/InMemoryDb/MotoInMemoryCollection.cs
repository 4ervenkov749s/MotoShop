using MotoShop.Models.DTO;
using System.Collections.Generic;

namespace MotoShop.DL.InMemoryDb
{
    public static class MotoInMemoryCollection
    {
        public static List<Moto> MotoDb = new List<Moto>()
        {
            new Moto()
            {
                Id = 1,
                Brand = "Ducati",
                Model = "999R"
            },
            new Moto()
            {
                Id=2,
                Brand = "Ducati",
                Model = "Panigale"
            },
            new Moto()
            {
                Id = 3,
                Brand = "Triumph",
                Model = "SpeedTriple"
            },
            new Moto()
            {
                Id = 4,
                Brand = "MVagusta",
                Model = "Brutale"
            }
        };

    }
}
