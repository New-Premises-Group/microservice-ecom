using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs
{
    public class CreateUser
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string Token { get; set; }
        public string? ImageURL { get; set; }
    }
}
