using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreatePaymentError;
using IW.Interfaces.Services;
using IW.Models.DTOs.PaymentDto;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class PaymentMutation
    {
        [Error(typeof(CreatePaymentErrorFactory))]
        public async Task<PaymentCreatedPayload> CreatePayment(CreatePayment input, [Service] IPaymentService paymentService)
        {
            await paymentService.CreatePayment(input);
            var payload = new PaymentCreatedPayload()
            {
                Message = "Payment successfully created"
            };
            return payload;
        }

        [Error(typeof(CreatePaymentErrorFactory))]
        public async Task<PaymentCreatedPayload> UpdatePayment(int id, UpdatePayment input, [Service] IPaymentService paymentService)
        {
            await paymentService.UpdatePayment(id, input);
            var payload = new PaymentCreatedPayload()
            {
                Message = "Payment successfully updated"
            };
            return payload;
        }
    }
}