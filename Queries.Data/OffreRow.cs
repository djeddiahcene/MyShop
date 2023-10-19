namespace Queries.Data
{
    public class OffreRow
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductSize { get; set; }
        public float PriceValue { get; set; }
        public float Quantity { get; set; }
    }
}