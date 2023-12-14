using FluentValidation;
using IW.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateOrderError;

namespace IW.Models.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName {  get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(15)")]
        public ORDER_STATUS Status { get; set; }
        public string ShippingAddress { get; set; }
        public string? CancelReason { get; set; }
        public decimal Total{ get; set; }
        public string? DiscountCode { get; set; } = string.Empty;
        public ICollection<OrderItem>? Items { get; set; }
        public float PointDeductionAmount { get; set; }
    }

    public class OrderValidator : GenericValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}");
            RuleFor(x => x.Date)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Must(BeAValidDate)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.BeAValidDate}")
                .LessThanOrEqualTo(DateTime.Now)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.LessThanOrEqualTo}");
            RuleFor(x=>x.Status)
                .IsInEnum()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.IsInEnum}");
            RuleFor(x=>x.ShippingAddress)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1,200)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(x => x.CancelReason)
                .Length(0, 200)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(x => x.Total)
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
        }
        public void ValidateAndThrowException(Order instance)
        {
            HandleValidateException(instance);
        }

        protected override void HandleValidateException(Order instance)
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
                throw new ValidateOrderException(validateErrors);
            }
        }
    }
}
