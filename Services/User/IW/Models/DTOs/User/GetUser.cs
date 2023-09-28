using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.User
{
    public class GetUser
    {
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
