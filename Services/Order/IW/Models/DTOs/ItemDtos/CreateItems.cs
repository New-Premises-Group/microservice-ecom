using IW.Interfaces.Commands;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.ItemDtos
{
    public class CreateItems:IRequest
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public IEnumerable<CreateItem> Items { get; set; }
    }
}
