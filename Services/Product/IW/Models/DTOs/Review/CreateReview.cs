using IW.Common;

namespace IW.Models.DTOs.Review
{
    public class CreateReview
    {
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public RATING Rating { get; set; }
        public string Detail { get; set; } = String.Empty;
        public DateTime? CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? UpdatedDate { get; set; } = DateTime.Now.ToUniversalTime();

    }
}
