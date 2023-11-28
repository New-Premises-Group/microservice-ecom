using IW.Common;
using IW.Handlers.Orders;
using IW.Interfaces.Commands;
using IW.Models.DTOs.OrderDtos;

namespace IW.Commands.Orders
{
    public class FinishOrderCommand : GenericCommand, ICommand<FinishOrder>
    {
        public FinishOrderCommand(
            FinishOrderHandler handler,
            FinishOrder request) : base(handler, request) { }
    }
}
