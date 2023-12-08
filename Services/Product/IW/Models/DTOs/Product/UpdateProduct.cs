using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.Product
{
    public class UpdateProduct
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Images { get; set; }
        public int CategoryId { get; set; }
    }
}
