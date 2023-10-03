using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.Inventory;
using IW.Models.DTOs.InventoryDto;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    [Authorize]
    public class InventoryQuery
    {
        public async Task<InventoryDto> GetInventory(int id, [Service] IInventoryService inventoryService)
        {
            var result = await inventoryService.GetInventory(id);
            return result;
        }

        public async Task<IEnumerable<InventoryDto>> GetInventories([Service] IInventoryService inventoryService,int offset = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await inventoryService.GetInventories(offset, amount);
            return results;
        }

        public async Task<IEnumerable<InventoryDto>> GetInventories(GetInvenory query, [Service] IInventoryService inventoryService, int offset = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await inventoryService.GetInventories(query,offset,amount);
            return results;
        }
    }
}
