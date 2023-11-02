using IW.Common;
using IW.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace IW.Models.DTOs.OrderDto
{
    public class OrderDto
    {
        public int? Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? Date { get; set; }
        public ORDER_STATUS? Status { get; set; }
        public string? ShippingAddress { get; set; }
        public string? CancelReason { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
    }
}
