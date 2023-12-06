using IW.Common;
using IW.Interfaces.Commands;
using IW.Models.DTOs.DiscountDtos;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.OrderDtos
{
    public class CreateGuestOrder:IRequest
    {
        [Required]
        public string Email{ get; set; }
        [Required]
        public string Phone{ get; set; }
        public string UserName { get; set; }
        public string? DiscountCode { get; set; }
        public DiscountConditionDto Condition { get; set; } = new DiscountConditionDto();
        public decimal Total { get; set; }
        [DefaultValue(ORDER_STATUS.Created)]
        public ORDER_STATUS Status { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public ICollection<CreateItem> Items { get; set; }
    }
}
