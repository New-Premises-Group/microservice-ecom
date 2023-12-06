using IW.Common;
using IW.Handlers.Discounts;
using IW.Handlers.Items;
using IW.Interfaces.Commands;
using IW.Models.DTOs.DiscountDtos;

namespace IW.Commands.Discounts
{
    public class ApplyDiscountCommand : GenericCommand, ICommand<ApplyDiscount>
    {
        public ApplyDiscountCommand(
            ApplyDiscountHandler handler,
            ApplyDiscount request) : base(handler, request) { }
    }
}
