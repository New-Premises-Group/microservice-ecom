using HotChocolate.Authorization;

namespace IW.Models.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        [Authorize]
        public string? Token { get; set; }
        public string? ImageURL { get; set; }
        public int RoleId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
