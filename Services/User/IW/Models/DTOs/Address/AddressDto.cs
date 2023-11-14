namespace IW.Models.DTOs.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsDefault { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
    }
}
