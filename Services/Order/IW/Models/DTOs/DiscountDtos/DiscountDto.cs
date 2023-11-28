using IW.Common;

namespace IW.Models.DTOs.DiscountDtos
{
    public class DiscountDto
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; } = string.Empty;
        public int? Amount { get; set; }
        public int? Quantity { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public DISCOUNT_TYPE Type { get; set; }
    }
}
