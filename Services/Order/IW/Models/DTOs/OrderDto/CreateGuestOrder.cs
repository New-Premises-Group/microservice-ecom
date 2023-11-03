using IW.Common;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.OrderDto
{
    public class CreateGuestOrder
    {
        [Required]
        public string Email{ get; set; }
        [Required]
        public string Phone{ get; set; }
        public decimal Total { get; set; }
        [DefaultValue(ORDER_STATUS.Created)]
        public ORDER_STATUS Status { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public IEnumerable<CreateItem> Items { get; set; }
    }
}
