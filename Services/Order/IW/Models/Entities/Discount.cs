using IW.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;
using FluentValidation;
using IW.Exceptions.CreateDiscountError;
using IW.Models.DTOs.DiscountDtos;
using StackExchange.Redis;

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
        public DISCOUNT_CONDITION Condition { get; set; } = DISCOUNT_CONDITION.None;
        public decimal TotalOverCondition { get; init; } = 0;
        public DateOnly BirthdayCondition { get; init; } = new DateOnly();
        public DateOnly SpecialDayCondition { get; init; } = new DateOnly();
        [NotMapped]
        public DiscountStrategy? Strategy { get; set; }
        [NotMapped]
        public static Discount Empty => new()
        {
            Id =-1,
            Code = "",
            Amount=0,
            Quantity=0,
        };

        public decimal Apply(decimal total, DiscountConditionDto condition)
        {
            SetStrategy();
            return Strategy.ApplyDiscount(total, Amount, condition)
;        }

        private void SetStrategy()
        {
            IDiscountCondition condition = Condition switch
            {
                DISCOUNT_CONDITION.None => new FreeForAllDiscount(),
                DISCOUNT_CONDITION.Birthday => new BirthdayDiscount(BirthdayCondition),
                DISCOUNT_CONDITION.SpecialDay => new SpecialdayDiscount(SpecialDayCondition),
                DISCOUNT_CONDITION.Total => new TotalBasedDiscount(TotalOverCondition),
                _ => new FreeForAllDiscount(),
            };

            Strategy = Type switch
            {
                DISCOUNT_TYPE.Percent => new PercentDiscountStrategy(condition),
                DISCOUNT_TYPE.Fixed => new FixedDiscountStrategy(condition),
                DISCOUNT_TYPE.Tier => new PercentDiscountStrategy(condition),
                _ => new NoDiscountStrategy(condition),
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
        private IDiscountCondition _condition;

        protected DiscountStrategy(IDiscountCondition condition)
        {
            _condition = condition;
        }

        public IDiscountCondition IDiscountCondition
        {
            get => default;
            set
            {
            }
        }

        public abstract decimal CalculateDiscount(decimal total, int discountAmount);

        public decimal ApplyDiscount(
            decimal total, 
            int discountAmount, 
            DiscountConditionDto condition)
        {
            if (CheckConditon(condition))
            {
                return CalculateDiscount(total, discountAmount);
            }

            return total;
        }

        public bool CheckConditon(DiscountConditionDto condition)
        {
            return _condition.IsApplicable(condition);
        }
    }

    public class PercentDiscountStrategy : DiscountStrategy
    {
        public PercentDiscountStrategy(IDiscountCondition condition) : base(condition)
        {
        }

        public override decimal CalculateDiscount(decimal total, int discountAmount)
        {
            return total - (total * discountAmount);
        }
    }

    public class FixedDiscountStrategy : DiscountStrategy
    {
        public FixedDiscountStrategy(IDiscountCondition condition) : base(condition)
        {
        }

        public override decimal CalculateDiscount(decimal total, int discountAmount)
        {
            return total - discountAmount;
        }
    }

    public class NoDiscountStrategy : DiscountStrategy
    {
        public NoDiscountStrategy(IDiscountCondition condition) : base(condition)
        {
        }

        public override decimal CalculateDiscount(decimal total, int discountAmount)
        {
            return total;
        }
    }

    public interface IDiscountCondition
    {
        public bool IsApplicable(DiscountConditionDto condition);   
    }

    public class TotalBasedDiscount : IDiscountCondition
    {
        private decimal _total;
        public TotalBasedDiscount(decimal total)
        {
            _total = total;
        }
        public bool IsApplicable(DiscountConditionDto condition)
        {
            return condition.Total > _total;
        }
    }

    public class BirthdayDiscount : IDiscountCondition
    {
        private DateOnly _birthday;
        public BirthdayDiscount(DateOnly date)
        {
            _birthday = date;
        }

        public bool IsApplicable(DiscountConditionDto condition)
        {
            return _birthday.Month == condition.Birthday?.Month;
        }
    }

    public class SpecialdayDiscount : IDiscountCondition
    {
        private DateOnly _specialDay;
        public SpecialdayDiscount(DateOnly date)
        {
            _specialDay = date;
        }

        public bool IsApplicable(DiscountConditionDto condition)
        {
            return DateOnly.Equals(_specialDay,condition.SpecialDay);
        }
    }

    public class FreeForAllDiscount : IDiscountCondition
    {
        public FreeForAllDiscount()
        {
        }

        public bool IsApplicable(DiscountConditionDto condition)
        {
            return true;
        }
    }
}
