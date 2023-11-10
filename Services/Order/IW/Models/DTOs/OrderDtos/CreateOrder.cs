using IW.Common;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.OrderDtos
{
    public class CreateOrder
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public decimal Total { get; set; }
        [DefaultValue(ORDER_STATUS.Created)]
        public ORDER_STATUS Status { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public ICollection<CreateItem> Items { get; set; }
    }
}
