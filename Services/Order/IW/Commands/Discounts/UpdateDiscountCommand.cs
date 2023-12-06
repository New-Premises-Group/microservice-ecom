using IW.Common;
using IW.Handlers.Discounts;
using IW.Handlers.Items;
using IW.Interfaces.Commands;
using IW.Models.DTOs.DiscountDtos;

namespace IW.Commands.Discounts
{
    public class UpdateDiscountCommand : GenericCommand, ICommand<UpdateDiscount>
    {
        public UpdateDiscountCommand(
            UpdateDiscountHandler handler,
            UpdateDiscount request) : base(handler, request) { }
    }
}
