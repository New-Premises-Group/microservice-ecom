using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.Role
{
    public class CreateRole
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
