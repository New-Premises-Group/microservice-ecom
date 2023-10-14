using IW.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs
{
    public class CreateCart
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
