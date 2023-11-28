using IW.Interfaces.Commands;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.OrderDtos
{
    public class FinishOrder : IRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
