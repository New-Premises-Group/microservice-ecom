using IW.Common;
using IW.Models.DTOs.Product;

namespace IW.Models.DTOs.Review
{
    public class ReviewDto
    {
        public int? Id { get; set; }
        public Guid? UserId { get; set; }
        public int? ProductId { get; set; }
        public RATING? Rating { get; set; }
        public string? Detail { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime UpdatedDate { get; set; } = DateTime.Now.ToUniversalTime();
        public string orderId { get; set; } = String.Empty;

    }
}
