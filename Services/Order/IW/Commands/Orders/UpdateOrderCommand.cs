using IW.Common;
using IW.Handlers.Orders;
using IW.Interfaces.Commands;
using IW.Models.DTOs.OrderDtos;

namespace IW.Commands.Orders
{
    public class UpdateOrderCommand : GenericCommand, ICommand<CreateOrder>
    {
        public UpdateOrderCommand(
            UpdateOrderHandler handler,
            UpdateOrder request) : base(handler, request) { }
    }
}
