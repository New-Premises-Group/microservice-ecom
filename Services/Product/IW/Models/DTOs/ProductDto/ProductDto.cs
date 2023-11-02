
using IW.Models.DTOs.CategoryDto;
using IW.Models.Entities;

namespace IW.Models.DTOs.Product
{ 
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Images { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto.CategoryDto Category { get; set; }
    }
}
