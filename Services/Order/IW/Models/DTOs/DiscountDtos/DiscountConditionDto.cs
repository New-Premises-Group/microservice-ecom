namespace IW.Models.DTOs.DiscountDtos
{
    public record DiscountConditionDto
    {
        public decimal? Total { get; init; } = 0;
        public DateOnly? Birthday { get; init; } = new DateOnly();
        public DateOnly? SpecialDay { get; init; } = new DateOnly();

    }
}
