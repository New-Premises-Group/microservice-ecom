using IW.Common;
using IW.Handlers.Orders;
using IW.Interfaces.Commands;
using IW.Models.DTOs.OrderDtos;

namespace IW.Commands.Orders
{
    public class DeleteOrderCommand : GenericCommand, ICommand<DeleteOrder>
    {
        public DeleteOrderCommand(
            DeleteOrderHandler handler,
            DeleteOrder request) : base(handler, request) { }
    }
}
