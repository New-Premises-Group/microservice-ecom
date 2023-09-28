using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.User
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
        public int RoleId { get; set; }
    }
}
