using FluentValidation;
using IW.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateCategoryError;

namespace IW.Models.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; }
    }

    internal class CategoryValidator : GenericValidator<Category>
    {
        public CategoryValidator()
        {

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1, 100)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(c => c.Description)
                .Length(1, 100)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
        }
        public void ValidateAndThrowException(Category category)
        {
            HandleValidateException(category);
        }

        protected override void HandleValidateException(Category instance)
        {
            ValidationResult results = Validate(instance);
            if (!results.IsValid)
            {
                List<ValidateErrorDetail> validateErrors = new();
                foreach (var failure in results.Errors)
                {
                    ValidateErrorDetail detail = new ValidateErrorDetail()
                    {
                        Property = failure.PropertyName,
                        Error = failure.ErrorMessage
                    };
                    validateErrors.Add(detail);
                }
                throw new ValidateCategoryException(validateErrors);
            }
        }
    }
}
