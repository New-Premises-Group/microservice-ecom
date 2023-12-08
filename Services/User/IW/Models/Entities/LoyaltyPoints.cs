using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.Entities
{
    public class LoyaltyPoints
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Point {  get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
