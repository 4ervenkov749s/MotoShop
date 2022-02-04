using MotoShop.Models.DTO;
using System.Collections.Generic;

namespace MotoShop.DL.InMemoryDb
{
    public static class PartInMemoryCollection
    {
        public static List<Part> PartDb = new List<Part>()
        {
            new Part()
            {
                Id = 1,
                Name = "ECU",
                PartNumber = "E789-2308-007",
                Price = 1875
            },
            new Part()
            {
                Id=2,
                Name = "BrakePads",
                PartNumber = "B891-003-721",
                Price = 250
            },
            new Part()
            {
                Id=3,
                Name = "Paint-1L",
                PartNumber = "P710-211-963",
                Price = 310
            },
            new Part()
            {
                Id=4,
                Name = "Engine",
                PartNumber = "E-N999-510-101",
                Price = 2500
            }
        };
    }
}
