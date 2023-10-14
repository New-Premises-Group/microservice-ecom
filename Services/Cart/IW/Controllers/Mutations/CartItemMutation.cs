using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateCartItemError;
using IW.Interfaces.Services;
using IW.Models.DTOs.CartItemDto;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
    public class CartItemMutation
    {
        [Error(typeof(CreateCartItemErrorFactory))]
        public async Task<CartItemCreatedPayload> CreateCartItem(CreateCartItem input, [Service] ICartItemService cartItemService)
        {
            await cartItemService.CreateCartItem(input);
            var payload = new CartItemCreatedPayload()
            {
                Message = "Cart item successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateCartItemErrorFactory))]
        public async Task<CartItemCreatedPayload> UpdateCartItem(int id, UpdateCartItem input, [Service] ICartItemService cartItemService)
        {
            await cartItemService.UpdateCartItem(id, input);
            var payload = new CartItemCreatedPayload()
            {
                Message = "Cart item successfully updated"
            };
            return payload;
        }
    }
}