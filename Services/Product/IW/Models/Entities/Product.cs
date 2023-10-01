using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using IW.Common;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateProductError;

namespace IW.Models.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId {  get; set; }
        public string SKU { get; set; }
        public string Images {  get; set; }
        public Category Category { get; set; }
    }

    internal class ProductValidator : GenericValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1, 20)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(x => x.Description)
                .Length(1, 100)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .GreaterThan(0)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.GreaterThan}");
            RuleFor(x => x.SKU)
                .Length(1, 50)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
        }

        public void ValidateAndThrowException(Product product)
        {
            HandleValidateException(product);
        }

        protected override void HandleValidateException(Product instance)
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
                throw new ValidateProductException(validateErrors);
            }
        }
    }
}
