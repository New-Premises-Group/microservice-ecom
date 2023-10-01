﻿using FluentValidation;
using IW.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;
using IW.Exceptions.CreateOrderError;

namespace IW.Models.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(15)")]
        public ORDER_STATUS Status { get; set; }
        public string ShippingAddress { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
    }

    internal class OrderValidator : GenericValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}");
            RuleFor(x => x.Date)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Must(BeAValidDate)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.BeAValidDate}")
                .LessThanOrEqualTo(DateTime.Now)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.LessThanOrEqualTo}");
            RuleFor(x=>x.Status)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .IsInEnum()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.IsInEnum}");
            RuleFor(x=>x.ShippingAddress)
                .NotEmpty()
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.NotEmpty}")
                .Length(1,200)
                .WithErrorCode($"{VALIDATOR_ERROR_CODE.Length}");
        }
        public void ValidateAndThrowException(Order instance)
        {
            HandleValidateException(instance);
        }

        protected override void HandleValidateException(Order instance)
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
                throw new ValidateOrderException(validateErrors);
            }
        }
    }
}