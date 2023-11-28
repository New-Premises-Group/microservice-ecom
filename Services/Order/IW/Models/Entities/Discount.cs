using IW.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;
using FluentValidation;
using IW.Exceptions.CreateDiscountError;

namespace IW.Models.Entities
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }= string.Empty;
        public int Amount { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpireDate { get; set; } = DateTime.Now;
        public DateTime ActiveDate { get; set; } = DateTime.Now;
        public DISCOUNT_TYPE Type { get; set; } = DISCOUNT_TYPE.None;
        [NotMapped]
        public DiscountStrategy? Strategy { get; set; }

        public decimal Apply(decimal total)
        {
            return Strategy.ApplyDiscount(total, Amount);
        }

        public void SetStrategy()
        {
            Strategy = Type switch
            {
                DISCOUNT_TYPE.Percent => new PercentDiscountStrategy(),
                DISCOUNT_TYPE.Fixed => new FixedDiscountStrategy(),
                DISCOUNT_TYPE.Tier => new PercentDiscountStrategy(),
                _ => new NoDiscountStrategy(),
            };
        }
    }

    public class DiscountValidator : GenericValidator<Discount>
    {
        public DiscountValidator()
        {
            RuleFor(x => x.Code)
                .Length(1, 50)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(x => x.Description)
                .Length(1, 200)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(x => x.ExpireDate)
                .Must(BeAValidDate)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.BeAValidDate}");
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThanOrEqualTo}");
            RuleFor(x => x.Type)
                .IsInEnum()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.IsInEnum}");
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
        }
        public void ValidateAndThrowException(Discount instance)
        {
            HandleValidateException(instance);
        }

        protected override void HandleValidateException(Discount instance)
        {
            ValidationResult results = Validate(instance);
            if (!results.IsValid)
            {
                List<ValidateErrorDetail> validateErrors = new();
                foreach (var failure in results.Errors)
                {
                    ValidateErrorDetail detail = new()
                    {
                        Property = failure.PropertyName,
                        Error = failure.ErrorMessage
                    };
                    validateErrors.Add(detail);
                }
                throw new ValidateDiscountException(validateErrors);
            }
        }
    }

    public abstract class DiscountStrategy
    {
        public abstract decimal ApplyDiscount(decimal total, int discountAmount);
    }

    public class PercentDiscountStrategy : DiscountStrategy
    {
        public override decimal ApplyDiscount(decimal total, int discountAmount)
        {
            return total - (total * discountAmount);
        }
    }

    public class FixedDiscountStrategy : DiscountStrategy
    {
        public override decimal ApplyDiscount(decimal total, int discountAmount)
        {
            return total - discountAmount;
        }
    }

    public class NoDiscountStrategy : DiscountStrategy
    {
        public override decimal ApplyDiscount(decimal total, int discountAmount)
        {
            return total;
        }
    }
}
