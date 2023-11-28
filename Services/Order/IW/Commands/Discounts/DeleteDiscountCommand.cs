using IW.Common;
using IW.Handlers.Discounts;
using IW.Interfaces.Commands;
using IW.Models.DTOs.DiscountDtos;

namespace IW.Commands.Discounts
{
    public class DeleteDiscountCommand : GenericCommand, ICommand<DeleteDiscount>
    {
        public DeleteDiscountCommand(
            DeleteDiscountHandler handler,
            DeleteDiscount request) : base(handler, request) { }
    }
}
