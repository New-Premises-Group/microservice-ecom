using IW.Interfaces.Commands;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.OrderDtos
{
    public class DeleteOrder : IRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
