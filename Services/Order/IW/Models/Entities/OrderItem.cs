using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using IW.Common;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateItemError;

namespace IW.Models.Entities
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId {  get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public int Quantity {  get; set; }
        public decimal Subtotal {  get; set; }
    }

    public sealed class ItemValidator : GenericValidator<OrderItem>
    {
        public ItemValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1, 20)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.SKU)
               .NotEmpty()
               .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
               .Length(1, 30)
               .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.Subtotal)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
        }

        public void ValidateAndThrowException(OrderItem instance)
        {
            HandleValidateException(instance);
        }

        protected override void HandleValidateException(OrderItem instance)
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
                throw new ValidateItemException(validateErrors);
            }
        }
    }
}
