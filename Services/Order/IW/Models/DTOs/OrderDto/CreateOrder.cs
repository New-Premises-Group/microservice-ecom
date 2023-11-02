using IW.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.OrderDto
{
    public class CreateOrder
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Email { get; set; }
        public decimal Total { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public IEnumerable<CreateItem> Items { get; set; }
    }
}
