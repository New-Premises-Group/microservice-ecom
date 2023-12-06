using IW.Common;
using IW.Handlers.Discounts;
using IW.Handlers.Items;
using IW.Interfaces.Commands;
using IW.Models.DTOs.DiscountDtos;

namespace IW.Commands.Discounts
{
    public class CreateDiscountCommand : GenericCommand, ICommand<CreateDiscount>
    {
        public CreateDiscountCommand(
            CreateDiscountHandler handler,
            CreateDiscount request) : base(handler, request) { }
    }
}
