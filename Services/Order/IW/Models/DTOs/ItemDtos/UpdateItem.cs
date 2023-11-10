
namespace IW.Models.DTOs.Item
{
    public class UpdateItem
    {
        public string? Name { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public string? SKU { get; set; }
        public int? Quantity { get; set; }
    }
}
