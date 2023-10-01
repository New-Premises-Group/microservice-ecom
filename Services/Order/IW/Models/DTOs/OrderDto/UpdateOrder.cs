using IW.Common;

namespace IW.Models.DTOs.OrderDto
{
    public class UpdateOrder
    {
        public ORDER_STATUS Status { get; set; }
        public string ShippingAddress { get; set; }
    }
}
