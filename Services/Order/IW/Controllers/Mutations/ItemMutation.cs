using IW.Interfaces;
using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateItemError;
using IW.Models.DTOs.Item;
using IW.Models.DTOs;
using IW.Models.DTOs.ItemPayloads;
using IW.Interfaces.Payloads;
using IW.Models.DTOs.ItemDtos;
using IW.Interfaces.Services;

namespace IW.MessageBroker.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize]
    public class ItemMutation
    {

        [Error(typeof(CreateItemErrorFactory))]
        public async Task<string> CreateItem(
            int orderId,
            CreateItem input, 
            [Service] IItemService itemService, 
            [Service] IItemPayloadFactory payloadFactory)
        {
            await itemService.CreateItem(orderId,input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Create);

            return payload.GetDetail($"Order-id:{orderId}");
        }

        [Error(typeof(CreateItemErrorFactory))]
        public async Task<string> UpdateItem(UpdateItem input,
            [Service] IMediator mediator, 
            [Service] IItemPayloadFactory payloadFactory)
        {
            var id=await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Create);

            return payload.GetDetail($"{id}");
        }

        [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
        public async Task<string> DeleteItem(
            DeleteItem input,
            [Service] IMediator mediator, 
            [Service] IItemPayloadFactory payloadFactory)
        {
            var id = await mediator.Send(input);
            var payload = payloadFactory.GetResponsePayload(PAYLOAD_TYPE.Create);
            
            return payload.GetDetail($"{id}");
        }
    }
}
