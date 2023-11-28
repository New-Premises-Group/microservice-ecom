using IW.Common;
using IW.Interfaces.Commands;
using IW.Models.DTOs.Item;

namespace IW.Models.DTOs.OrderDtos
{
    public class OrderDto : IRequest
    {
        public int? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public DateTime? Date { get; set; }
        public ORDER_STATUS? Status { get; set; }
        public string? ShippingAddress { get; set; }
        public string? CancelReason { get; set; }
        public decimal Total { get; set; }
        public ICollection<ItemDto>? Items { get; set; }
    }
}
