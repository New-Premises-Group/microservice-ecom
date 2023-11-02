using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using IW.Common;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateRoleError;

namespace IW.Models.Entities
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<User>? Users { get; set; }
    }
    public class RoleValidator : GenericValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1, 20)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}"); 
            RuleFor(x => x.Description)
                .Length(1, 100)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
        }

        public void ValidateAndThrowException(Role user)
        {
            HandleValidateException(user);
        }

        protected override void HandleValidateException(Role instance)
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
                throw new ValidateRoleException(validateErrors);
            }
        }
    }
}
