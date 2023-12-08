using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs
{
    public class CreateProduct
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string Images { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
