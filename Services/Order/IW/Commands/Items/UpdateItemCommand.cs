using IW.Common;
using IW.Handlers.Items;
using IW.Interfaces.Commands;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.ItemDtos;
using IW.Models.DTOs.OrderDtos;

namespace IW.Commands.Items
{
    public class UpdateItemCommand : GenericCommand, ICommand<UpdateItem>
    {
        public UpdateItemCommand(
            UpdateItemHandler handler,
            UpdateItem request) : base(handler, request) { }
    }
}
