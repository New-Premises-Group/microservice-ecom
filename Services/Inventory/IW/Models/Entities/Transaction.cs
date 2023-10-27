using FluentValidation;
using IW.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateTransactionError;

namespace IW.Models.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(15)")]
        public TRANSACTION_TYPE Type { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
    }

    internal class TransactionValidator : GenericValidator<Transaction>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.InventoryId)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}");
            RuleFor(x => x.Date)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Must(BeAValidDate)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.BeAValidDate}")
                .LessThanOrEqualTo(DateTime.Now)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.LessThanOrEqualTo}");
            RuleFor(x=>x.Type)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .IsInEnum()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.IsInEnum}");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}");
            RuleFor(x => x.Note)
                .MaximumLength(200)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.MaxLength}");
        }
        public void ValidateAndThrowException(Transaction instance)
        {
            HandleValidateException(instance);
        }

        protected override void HandleValidateException(Transaction instance)
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
                throw new ValidateTransactionException(validateErrors);
            }
        }
    }
}
