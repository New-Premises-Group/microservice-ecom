using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.Point
{
    public class UpdatePoint
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Point { get; set; }
    }
}
