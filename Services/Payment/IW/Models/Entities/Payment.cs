using FluentValidation;
using IW.Common;
using IW.Exceptions.CreatePaymentError;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace IW.Models.Entities
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int OrderID { get; set; }
        public Guid UserID { get; set; }
        public DateTime Date { get; set; }
        public PAYMENT_STATUS Status { get; set; }
        public decimal Amount { get; set; }
        //default is VND
        public CURRENCY Currency { get; set; }
        public PAYMENT_TYPE PaymentMethod { get; set; }
        public string TransactionsReference { get; set; }

    }

    internal class PaymentValidator : GenericValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.ID)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.OrderID)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.IsInEnum}");
            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.Currency)
                .IsInEnum()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.IsInEnum}");
            RuleFor(x => x.PaymentMethod)
                .IsInEnum()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.IsInEnum}");
            RuleFor(x => x.TransactionsReference)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}");
        }

        public void ValidateAndThrowException(Payment instance)
        {
            HandleValidateException(instance);
        }

        protected override void HandleValidateException(Payment instance)
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
                throw new ValidatePaymentException(validateErrors);
            }
        }
    }
}