using IW.Common;
using IW.Handlers.Items;
using IW.Interfaces.Commands;
using IW.Models.DTOs.ItemDtos;
using IW.Models.DTOs.OrderDtos;

namespace IW.Commands.Items
{
    public class DeleteItemCommand : GenericCommand, ICommand<DeleteItem>
    {
        public DeleteItemCommand(
            DeleteItemHandler handler,
            DeleteItem request) : base(handler, request) { }
    }
}
