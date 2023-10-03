using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using IW.Common;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateInventoryError;

namespace IW.Models.Entities
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public bool Availability { get; set; }
        public int Quantity {  get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }

    internal class InventoryValidator : GenericValidator<Inventory>
    {
        public InventoryValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.Availability)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Must(p => p.GetType() == typeof(bool))
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.MustBeBoolean}");
        }

        public void ValidateAndThrowException(Inventory instance)
        {
            HandleValidateException(instance);
        }

        protected override void HandleValidateException(Inventory instance)
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
                throw new ValidateInventoryException(validateErrors);
            }
        }
    }
}
