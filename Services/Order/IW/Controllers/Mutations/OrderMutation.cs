using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateOrderError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.OrderDto;

namespace IW.MessageBroker.Mutations
{
    [ExtendObjectType("Mutation")]
    [Authorize]
    public class OrderMutation
    {
        [Error(typeof(CreateOrderErrorFactory))]
        public async Task<OrderCreatedPayload> CreateOrder(CreateOrder input, [Service] IOrderService orderService)
        {
            int orderId=await orderService.CreateOrder(input);
            var payload = new OrderCreatedPayload()
            {
                Message = "Order successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateOrderErrorFactory))]
        [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
        public async Task<OrderCreatedPayload> UpdateOrder(int id, UpdateOrder input, [Service] IOrderService orderService)
        {
            await orderService.UpdateOrder(id, input);
            var payload = new OrderCreatedPayload()
            {
                Message = "Order successfully updated"
            };
            return payload;
        }
    }
}
