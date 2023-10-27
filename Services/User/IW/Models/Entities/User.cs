using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using IW.Common;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateUserError;

namespace IW.Models.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Token { get; set; }
        public string? ImageURL {  get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId {  get; set; }
        public Role Role { get; set; }
        public ICollection<Address> Addresses { get; set; }

    }

    public class UserValidator : GenericValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user=>user.Email)
                .Matches(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$")
                .WithErrorCode($"{ VALIDATOR_ERROR_CODE.Match}");

            RuleFor(user=>user.Name)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1,50)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");

            RuleFor(user=>user.Token)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}");

            RuleFor(user => user.PhoneNumber)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Matches("0?\\W*([1-9][0-9]{2})\\W*([0-9]{2,3})\\W*?([0-9]{3})(\\se?x?t?(\\d*))?")
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Match}");
        }
        public void ValidateAndThrowException(User user)
        {
            HandleValidateException(user);
        }

        protected override void HandleValidateException(User instance)
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
                throw new ValidateUserException(validateErrors);
            }
        }
    }
}
