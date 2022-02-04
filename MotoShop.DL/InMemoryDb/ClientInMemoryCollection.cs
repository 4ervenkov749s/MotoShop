using MotoShop.Models.DTO;
using System.Collections.Generic;


namespace MotoShop.DL.InMemoryDb
{
    public static class ClientInMemoryCollection
    {
        public static List<Client> ClientDb = new List<Client>()
        {
            new Client()
            {
                Id = 1,
                Name ="Senq",
                Moto = new List<Moto>()
                {
                    new Moto()
                    {
                        Id=1,
                        Brand = "Ducati",
                        Model = "749s"
                    }
                },
                Discount = 9,
                PaymentType = Models.Enums.PaymentType.Cash
            },
            new Client()
            {
                Id=2,
                Name = "Alexander",
                Moto=new List<Moto>()
                {
                    new Moto()
                    {
                        Id=2,
                        Brand = "BMW",
                        Model = "R1200"
                    }
                },
                Discount=5,
                PaymentType = Models.Enums.PaymentType.CreditCard
            },
            new Client()
            {
                Id=3,
                Name = "Nikolai",
                Moto = new List<Moto>()
                {
                    new Moto()
                    {
                        Id=3,
                        Brand = "Honda",
                        Model = "CBR1000"
                    }
                },
                Discount=1,
                PaymentType = Models.Enums.PaymentType.Cash
            },

        };
    }
}
