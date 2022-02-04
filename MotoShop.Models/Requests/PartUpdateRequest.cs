namespace MotoShop.Models.Requests
{
    public class PartUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public double Price { get; set; }

    }
}
