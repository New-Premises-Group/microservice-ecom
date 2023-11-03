using HotChocolate.Authorization;
using IW.Common;
using IW.Interfaces.Services;
using IW.Models.DTOs.OrderDto;

namespace IW.MessageBroker.Queries
{
    [ExtendObjectType("Query")]
    [Authorize]
    public class OrderQuery
    {
        [Authorize(Roles = new[] { nameof(ROLE.Admin) })]
        public async Task<IEnumerable<OrderDto>> GetOrders([Service] IOrderService orderService, int page = (int)PAGINATING.OffsetDefault, int amount = (int)PAGINATING.AmountDefault)
        {
            var results = await orderService.GetOrders(page,amount);
            return results;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(GetOrder query,[Service] IOrderService orderService, int page = (int)PAGINATING.OffsetDefault, int amount = (int)PAGINATING.AmountDefault)
        {
            var results = await orderService.GetOrders(query, page, amount);
            return results;
        }

        [AllowAnonymous]
        public async Task<OrderDto> GetOrder(int id, [Service] IOrderService orderService)
        {
            var result = await orderService.GetOrder(id);
            return result;
        }
    }
}
