using IW.Common;

namespace IW.Models.DTOs.Review
{
    public class UpdateReview
    {
        public int Id { get; set; }
        public RATING? Rating { get; set; }
        public string? Detail { get; set; } = String.Empty;
    }
}
