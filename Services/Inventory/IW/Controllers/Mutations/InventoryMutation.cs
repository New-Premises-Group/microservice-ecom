using IW.Interfaces;
using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateInventoryError;
using IW.Models.DTOs.Inventory;
using IW.Models.DTOs;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class InventoryMutation
    {
        [Error(typeof(CreateInventoryErrorFactory))]
        public async Task<InventoryCreatedPayload> CreateInventory(CreateInventory input, [Service] IInventoryService inventoryService)
        {
            await inventoryService.CreateInventory(input);
            var payload = new InventoryCreatedPayload()
            {
                Message = "Inventory successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateInventoryErrorFactory))]
        public async Task<InventoryUpdatedPayload> UpdateInventory(int id, UpdateInventory input, [Service] IInventoryService inventoryService)
        {
            await inventoryService.UpdateInventory(id, input);
            var payload = new InventoryUpdatedPayload()
            {
                Message = "Inventory successfully updated",
            };
            return payload;
        }

        public async Task<InventoryDeletedPayload> DeleteInventory(int id, [Service] IInventoryService inventoryService)
        {
            await inventoryService.DeleteInventory(id);
            var payload = new InventoryDeletedPayload()
            {
                Message = "Inventory successfully deleted"
            };
            return payload;
        }
    }
}
