using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.CartItemDto
{
    public class CreateCartItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal Subtotal => Price * Quantity;
    }
}