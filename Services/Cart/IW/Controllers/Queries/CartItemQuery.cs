using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces.Services;
using IW.Models.DTOs.CartItemDto;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class CartItemQuery
    {
        public async Task<IEnumerable<CartItemDto>> GetCartItems([Service] ICartItemService cartItemService, int offset = (int)PAGINATING.OffsetDefault, int amount = (int)PAGINATING.AmountDefault)
        {
            var results = await cartItemService.GetCartItems(offset, amount);
            return results;
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItems(GetCartItem query, [Service] ICartItemService cartItemService, int offset = (int)PAGINATING.OffsetDefault, int amount = (int)PAGINATING.AmountDefault)
        {
            var results = await cartItemService.GetCartItems(query, offset, amount);
            return results;
        }

        public async Task<CartItemDto> GetCartItem(int id, [Service] ICartItemService cartItemService)
        {
            var result = await cartItemService.GetCartItem(id);
            return result;
        }
    }
}