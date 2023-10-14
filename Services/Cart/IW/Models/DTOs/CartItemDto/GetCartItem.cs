namespace IW.Models.DTOs.CartItemDto
{
    public class GetCartItem
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}