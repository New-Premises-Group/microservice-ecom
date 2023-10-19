using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces.Services;
using IW.Models.DTOs.PaymentDto;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class PaymentQuery
    {
        public async Task<IEnumerable<PaymentDto>> GetPayments([Service] IPaymentService paymentService, int offset = (int)PAGINATING.OffsetDefault, int amount = (int)PAGINATING.AmountDefault)
        {
            var results = await paymentService.GetPayments(offset, amount);
            return results;
        }

        public async Task<IEnumerable<PaymentDto>> GetPayments(GetPayment query, [Service] IPaymentService paymentService, int offset = (int)PAGINATING.OffsetDefault, int amount = (int)PAGINATING.AmountDefault)
        {
            var results = await paymentService.GetPayments(query, offset, amount);
            return results;
        }

        public async Task<PaymentDto> GetPayment(int id, [Service] IPaymentService paymentService)
        {
            var result = await paymentService.GetPayment(id);
            return result;
        }
    }
}