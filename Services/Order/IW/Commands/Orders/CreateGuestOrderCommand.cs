using IW.Common;
using IW.Handlers.Orders;
using IW.Interfaces.Commands;
using IW.Models.DTOs.OrderDtos;

namespace IW.Commands.Orders
{
    public class CreateGuestOrderCommand : GenericCommand,ICommand<CreateGuestOrder>
    {
        public CreateGuestOrderCommand(
            CreateGuestOrderHandler handler,
            CreateGuestOrder request) : base(handler, request)
        {

        }
    }
}
