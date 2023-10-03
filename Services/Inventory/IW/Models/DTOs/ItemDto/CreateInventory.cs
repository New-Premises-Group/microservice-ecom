using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs
{
    public class CreateInventory
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public bool Availability { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
