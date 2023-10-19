namespace Domain.Entities
{
    public class Product : Entity
    {
        public string ProductName { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductSize { get; set; }

        public Price Price { get; set; }
        public Stock Stock { get; set; }
    }
}
