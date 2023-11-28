using IW.Interfaces.Commands;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.ItemDtos
{
    public class DeleteItem:IRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
