using IW.Common;
using IW.Interfaces.Commands;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.OrderDtos
{
    public class UpdateOrder : IRequest
    {
        [Required]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public ORDER_STATUS? Status { get; set; }
        public string? CancelReason { get; set; }
        public string ShippingAddress { get; set; }
    }
}
