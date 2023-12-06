using IW.Common;
using IW.Interfaces.Commands;
using IW.Models.Entities;

namespace IW.Models.DTOs.OrderDtos
{
    public class GetOrder : IRequest
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public DateTime? Date { get; set; }
        public ORDER_STATUS? Status { get; set; }
    }
}
