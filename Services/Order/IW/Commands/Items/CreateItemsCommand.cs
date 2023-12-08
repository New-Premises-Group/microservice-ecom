using IW.Common;
using IW.Handlers.Items;
using IW.Handlers.Orders;
using IW.Interfaces.Commands;
using IW.Models.DTOs.ItemDtos;
using IW.Models.DTOs.OrderDtos;

namespace IW.Commands.Items
{
    public class CreateItemsCommand : GenericCommand, ICommand<CreateItems>
    {
        public CreateItemsCommand(
            CreateItemsHandler handler,
            CreateItems request) : base(handler, request) { }

        public CreateItemsHandler CreateItemsHandler
        {
            get => default;
            set
            {
            }
        }
    }
}
