using IW.Interfaces;
using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateItemError;
using IW.Models.DTOs.Item;
using IW.Models.DTOs;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize]
    public class ItemMutation
    {
        [Error(typeof(CreateItemErrorFactory))]
        public async Task<ItemCreatedPayload> CreateItem(CreateItem input, [Service] IItemService itemService)
        {
            await itemService.CreateItem(input);
            var payload = new ItemCreatedPayload()
            {
                Message = "Item successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateItemErrorFactory))]
        public async Task<ItemUpdatedPayload> UpdateItem(int id, UpdateItem input, [Service] IItemService itemService)
        {
            await itemService.UpdateItem(id, input);
            var payload = new ItemUpdatedPayload()
            {
                Message = "Item successfully updated",
            };
            return payload;
        }

        [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
        public async Task<ItemDeletedPayload> DeleteItem(int id, [Service] IItemService itemService)
        {
            await itemService.DeleteItem(id);
            var payload = new ItemDeletedPayload()
            {
                Message = "Item successfully deleted"
            };
            return payload;
        }
    }
}
