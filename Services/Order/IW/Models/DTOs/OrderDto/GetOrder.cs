using IW.Common;
using IW.Models.Entities;

namespace IW.Models.DTOs.OrderDto
{
    public class GetOrder
    {
        public Guid? UserId { get; set; }
        public DateTime? Date { get; set; }
        public ORDER_STATUS? Status { get; set; }
    }
}
