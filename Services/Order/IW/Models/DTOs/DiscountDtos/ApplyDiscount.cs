using IW.Common;
using IW.Interfaces.Commands;
using IW.Models.Entities;

namespace IW.Models.DTOs.DiscountDtos
{
    public class ApplyDiscount : IRequest
    {
        public string Code { get; set; }
        public Order Order { get; set; }
        public DiscountConditionDto Condition { get; set; } = new DiscountConditionDto();
    }
}
