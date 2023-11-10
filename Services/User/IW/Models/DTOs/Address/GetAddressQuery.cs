namespace IW.Models.DTOs.Address
{
    public class GetAddressQuery
    {
        public Guid? UserId { get; set; }
        public string? Phone { get; set; }
        public string? Name { get; set; }
    }
}
