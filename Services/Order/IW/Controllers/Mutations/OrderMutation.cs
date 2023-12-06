using HotChocolate.Authorization;
using IW.Commands.Orders;
using IW.Common;
using IW.Exceptions.CreateOrderError;
using IW.Interfaces.Payloads;
using IW.Interfaces.Services;
using IW.Models.DTOs.OrderDtos;
using IW.Services;

namespace IW.MessageBroker.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize]
    public class OrderMutation
    {
        [Error(typeof(CreateOrderErrorFactory))]
        public async Task<string> CreateOrder(CreateOrder input,
            [Service] IMediator mediator, 
            [Service]IOrderPayloadFactory payloadFactory)
        {
            int orderId = await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Create);
            
            return payload.GetDetail($"orderId:{orderId}");
        }

        [Error(typeof(CreateOrderErrorFactory))]
        [AllowAnonymous]
        public async Task<string> CreateGuestOrder(CreateGuestOrder input,
            [Service] IOrderPayloadFactory payloadFactory,
            [Service] IMediator mediator)
        {
            int orderId = await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Create);

            return payload.GetDetail($"orderId:{orderId}");
        }

        [Error(typeof(CreateOrderErrorFactory))]
        [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
        public async Task<string> UpdateOrder(
            UpdateOrder input,
            [Service] IMediator mediator, 
            [Service] IOrderPayloadFactory payloadFactory)
        {
            int orderId = await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Update);

            return payload.GetDetail($"orderId:{orderId}");
        }

        public async Task<string> FinishOrder(DeleteOrder input,
            [Service] IMediator mediator,
            [Service] IOrderPayloadFactory payloadFactory)
        {
            int orderId = await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Update);

            return payload.GetDetail($"orderId:{orderId}");
        }

        public async Task<string> DeleteOrder(FinishOrder input,
            [Service] IMediator mediator,
            [Service] IOrderPayloadFactory payloadFactory)
        {
            int orderId = await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Delete);

            return payload.GetDetail($"orderId:{orderId}");
        }
    }
}
