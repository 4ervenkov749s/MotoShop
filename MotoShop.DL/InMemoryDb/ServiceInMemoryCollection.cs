using MotoShop.Models.DTO;
using System.Collections.Generic;

namespace MotoShop.DL.InMemoryDb
{
    public static class ServiceInMemoryCollection
    {
        public static List<Service> ServiceDb = new List<Service>()
        {
            new Service()
            {
                Id = 1,
                Name = "Engine-1hour",
                Price = 150
            },
            new Service()
            {
                Id=2,
                Name = "Electronic-1hour",
                Price = 250
            },
            new Service()
            {
                Id=3,
                Name ="Gearbox-1hour",
                Price = 200
            },
            new Service()
            {
                Id = 4,
                Name = "Paint-1hour",
                Price = 100
            }
        };
    }
}
