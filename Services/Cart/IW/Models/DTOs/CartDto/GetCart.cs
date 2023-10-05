

using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.Cart
{
    public class GetCart
    {
        [Required]
        public Guid? UserId { get; set; }
    }
}
