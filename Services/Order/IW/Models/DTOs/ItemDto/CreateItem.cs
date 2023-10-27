using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs
{
    public class CreateItem
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
