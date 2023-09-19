namespace IW.Models.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Token { get; set; }
        public string? ImageURL { get; set; }
        public int RoleId { get; set; }
    }
}
