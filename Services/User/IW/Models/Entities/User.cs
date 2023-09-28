using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using IW.Common;
using FluentValidation.Results;
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
        public int RoleId {  get; set; }
        public Role Role { get; set; }
        
    }

    public class UserValidator : GenericValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user=>user.Email)
                .Matches(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$")
                .WithErrorCode($"{ ValidatorErrorCode.Match}");

            RuleFor(user=>user.Name)
                .NotEmpty()
                .WithErrorCode($"{ValidatorErrorCode.NotEmpty}")
                .Length(1,50)
                .WithErrorCode($"{ValidatorErrorCode.Length}");

            RuleFor(user=>user.Token)
                .NotEmpty()
                .WithErrorCode($"{ValidatorErrorCode.NotEmpty}");
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
                List<ValidateErrorDetail> validateErrors = new List<ValidateErrorDetail>();
                foreach (var failure in results.Errors)
                {
                    ValidateErrorDetail detail = new ValidateErrorDetail()
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
