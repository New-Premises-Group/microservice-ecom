
namespace IW.Models.DTOs.ItemDto
{
    public class GetItem
    {
        public int? OrderId { get; set; }
        public string? Name { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
    }
}
