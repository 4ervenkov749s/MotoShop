using MotoShop.Models.DTO;
using MotoShop.Models.Enums;
using System.Collections.Generic;

namespace MotoShop.Models.Requests
{
    public class ClientRequest
    {
        public string Name { get; set; }
        public PaymentType PaymentType { get; set; }
        public int Discount { get; set; }
        public List<Moto> Moto { get; set; }

    }
}
