using FluentValidation;
using IW.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateAddressError;

namespace IW.Models.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Phone {  get; set; }
        public string Name {  get; set; }
        public string Detail { get; set; }
        public string Ward {  get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }

    public class AddressValidator : GenericValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Detail)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1, 100)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(address => address.Detail)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1, 100)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(address => address.Name)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1, 100)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");

            RuleFor(address => address.Phone)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Matches("^0([1-9][0-9]{2})([0-9]{2,3})([0-9]{3})$")
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Match}");

            RuleFor(address => address.District)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}");

            RuleFor(address => address.City)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}");
        }
        public void ValidateAndThrowException(Address address)
        {
            HandleValidateException(address);
        }

        protected override void HandleValidateException(Address instance)
        {
            ValidationResult results = Validate(instance);
            if (!results.IsValid)
            {
                List<ValidateErrorDetail> validateErrors = new();
                foreach (var failure in results.Errors)
                {
                    ValidateErrorDetail detail = new ()
                    {
                        Property = failure.PropertyName,
                        Error = failure.ErrorMessage
                    };
                    validateErrors.Add(detail);
                }
                throw new ValidateAddressException(validateErrors);
            }
        }
    }
}
