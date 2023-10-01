using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.CategoryDto
{
    public class CreateCategory
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
