using IW.Common;

namespace IW.Models.DTOs.OrderDtos
{
    public class UpdateOrder
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
        public ORDER_STATUS? Status { get; set; }
        public string? CancelReason { get; set; }
        public string ShippingAddress { get; set; }
    }
}
