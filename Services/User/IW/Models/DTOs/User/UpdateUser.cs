using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.User
{
    public class UpdateUser
    {
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? ImageURL { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
