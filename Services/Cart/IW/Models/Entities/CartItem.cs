using FluentValidation;
using IW.Common;
using IW.Exceptions.CreateCartItemError;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace IW.Models.Entities
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public decimal Subtotal { get; set; }
    }

    internal class CartItemValidator : GenericValidator<CartItem>
    {
        public CartItemValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1, 100)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}")
                .Matches("^[A-Za-z0-9\\s\\-]*$")
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Match}");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1, 100)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}")
                .Matches("^[A-Za-z0-9\\s\\-]*$")
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Match}");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
        }

        public void ValidateAndThrowException(CartItem instance)
        {
            HandleValidateException(instance);
        }

        protected override void HandleValidateException(CartItem instance)
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
                throw new ValidateCartItemException(validateErrors);
            }
        }
    }
}