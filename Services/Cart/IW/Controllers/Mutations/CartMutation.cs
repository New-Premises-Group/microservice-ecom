using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateCartError;
using IW.Interfaces.Services;
using IW.Models.DTOs;
using IW.Models.DTOs.Cart;

namespace IW.Controllers.Mutations
{
    [ExtendObjectType("Mutation")]
    public class CartMutation
    {
        [Error(typeof(CreateCartErrorFactory))]
        public async Task<CartCreatedPayload> CreateCart(CreateCart input, [Service] ICartService cartService)
        {
            await cartService.CreateCart(input);
            var payload = new CartCreatedPayload()
            {
                Message = "Cart successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateCartErrorFactory))]
        public async Task<CartUpdatedPayload> UpdateCart(int id, UpdateCart input, [Service] ICartService cartService)
        {
            await cartService.UpdateCart(id, input);
            var payload = new CartUpdatedPayload()
            {
                Message = "Cart successfully updated",
            };
            return payload;
        }

        public async Task<CartDeletedPayload> DeleteCart(int id, [Service] ICartService cartService)
        {
            await cartService.DeleteCart(id);
            var payload = new CartDeletedPayload()
            {
                Message = "Cart successfully deleted"
            };
            return payload;
        }
    }
}