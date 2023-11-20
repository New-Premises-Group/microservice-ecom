using HotChocolate.Authorization;
using IW.Common;
using IW.Exceptions.CreateOrderError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.OrderDtos;
using IW.Services;

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
        [AllowAnonymous]
        public async Task<OrderCreatedPayload> CreateGuestOrder(CreateGuestOrder input, [Service] IOrderService orderService)
        {
            int orderId = await orderService.CreateGuestOrder(input);
            var payload = new OrderCreatedPayload()
            {
                Message = "Order successfully created"
            };
            return payload;
        }

        [Error(typeof(CreateOrderErrorFactory))]
        [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
        public async Task<OrderUpdatedPayload> UpdateOrder(int id, UpdateOrder input, [Service] IOrderService orderService)
        {
            await orderService.UpdateOrder(id, input);
            var payload = new OrderUpdatedPayload()
            {
                Message = "Order successfully updated"
            };
            return payload;
        }

        public async Task<OrderUpdatedPayload> FinishOrder(int id, [Service] IOrderService orderService)
        {
            await orderService.FinishOrder(id);
            return new OrderUpdatedPayload()
            {
                Message = "Finish Order Successfully"
            };
        }

        public async Task<OrderDeletedPayload> DeleteOrder(int id, [Service] IOrderService orderService)
        {
            await orderService.DeleteOrder(id);
            var payload = new OrderDeletedPayload()
            {
                Message = "Order successfully deleted"
            };
            return payload;
        }
    }
}
