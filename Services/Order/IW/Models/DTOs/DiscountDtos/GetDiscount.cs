using IW.Common;

namespace IW.Models.DTOs.DiscountDtos
{
    public class GetDiscount
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public DateTime ExpireDate { get; set; }
        public DISCOUNT_TYPE Type { get; set; }
    }
}
