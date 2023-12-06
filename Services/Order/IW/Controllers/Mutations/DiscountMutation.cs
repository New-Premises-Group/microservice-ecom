using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateDiscountError;
using IW.Exceptions.CreateOrderError;
using IW.Interfaces.Payloads;
using IW.Interfaces.Services;
using IW.Models.DTOs.DiscountDtos;
using IW.Models.DTOs.OrderDtos;

namespace IW.MessageBroker.Mutations
{
    [ExtendObjectType("Mutation")]
    public class DiscountMutation
    {
        [Error(typeof(CreateDiscountErrorFactory))]
        [AllowAnonymous]
        public async Task<string> CreateDiscount(CreateDiscount input,
            [Service] IMediator mediator, 
            [Service]IOrderPayloadFactory payloadFactory)
        {
            int orderId = await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Create);
            
            return payload.GetDetail($"orderId:{orderId}");
        }

        [Error(typeof(CreateOrderErrorFactory))]
        //[Authorize(Roles = new[] { nameof(ROLE.Admin) })]
        [AllowAnonymous]
        public async Task<string> UpdateDiscount(
            UpdateDiscount input,
            [Service] IMediator mediator, 
            [Service] IOrderPayloadFactory payloadFactory)
        {
            int orderId = await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Update);

            return payload.GetDetail($"orderId:{orderId}");
        }

        [AllowAnonymous]
        public async Task<string> DeleteDiscount(DeleteDiscount input,
            [Service] IMediator mediator,
            [Service] IOrderPayloadFactory payloadFactory)
        {
            int orderId = await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Delete);

            return payload.GetDetail($"orderId:{orderId}");
        }
    }
}
