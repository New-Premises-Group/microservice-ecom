using IW.Common;
using IW.Handlers.Orders;
using IW.Interfaces.Commands;
using IW.Models.DTOs.OrderDtos;

namespace IW.Commands.Orders
{
    public class CreateOrderCommand : GenericCommand, ICommand<CreateOrder>
    {
        public CreateOrderCommand(
            CreateOrderHandler handler,
            CreateOrder request) : base(handler, request) { }
    }
}
