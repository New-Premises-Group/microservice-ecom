namespace IW.Models.DTOs.Address
{
    public class CreateAddress
    {
        public Guid UserId { get; set; }
        public string Detail { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
    }
}
