using IW.Models.Entities;

namespace IW.Models.DTOs.Item
{ 
    public class ItemDto
    {
        public int? Id { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? SKU { get; set; }
        public int? Quantity { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
