using IW.Common;
using IW.Models.DTOs.Product;

namespace IW.Models.DTOs.Review
{
    public class GetReview
    {
        public Guid? UserId { get; set; }
        public int? ProductId { get; set; }
        public RATING? Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
