using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces.Services;
using IW.Models.DTOs.Cart;

namespace IW.Controllers.Queries
{
    [ExtendObjectType("Query")]
    public class CartQuery
    {
        public async Task<CartDto> GetCart(int id, [Service] ICartService cartService)
        {
            var result = await cartService.GetCart(id);
            return result;
        }
        [AllowAnonymous]
        public async Task<IEnumerable<CartDto>> GetCarts([Service] ICartService cartService, int offset = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await cartService.GetCarts(offset, amount);
            return results;
        }

        public async Task<IEnumerable<CartDto>> GetCarts(GetCart query, [Service] ICartService cartService, int offset = ((int)PAGINATING.OffsetDefault), int amount = ((int)PAGINATING.AmountDefault))
        {
            var results = await cartService.GetCarts(query, offset, amount);
            return results;
        }
    }
}