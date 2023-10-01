using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.ItemDto;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    [Authorize]
    public class ItemQuery
    {
        public async Task<ItemDto> GetItem(int id, [Service] IItemService itemService)
        {
            var result = await itemService.GetItem(id);
            return result;
        }

        public async Task<IEnumerable<ItemDto>> GetItems([Service] IItemService itemService,int offset = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await itemService.GetItems(offset, amount);
            return results;
        }

        public async Task<IEnumerable<ItemDto>> GetItems(GetItem query, [Service] IItemService itemService, int offset = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await itemService.GetItems(query,offset,amount);
            return results;
        }
    }
}
