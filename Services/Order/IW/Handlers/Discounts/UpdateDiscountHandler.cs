using IW.Common;
using IW.Exceptions.ReadItemError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.DiscountDtos;
using IW.Models.Entities;
using MapsterMapper;

namespace IW.Handlers.Discounts
{
    public class UpdateDiscountHandler : AbstractCommandHandler
    {
        private Discount? _originalDiscount;
        public UpdateDiscountHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(UpdateDiscount request)
        {
            var discount = await _unitOfWork.Discounts.GetById(request.Id);
            if (Equals(discount, null)) throw new ItemNotFoundException(request.Id);
            _originalDiscount = discount;

            discount.Code=request.Code ?? discount.Code;
            discount.Description=request.Description ?? discount.Description;
            discount.ExpireDate = request.ExpireDate;
            discount.ActiveDate = request.ActiveDate;
            discount.Amount=request.Amount ?? discount.Amount;
            discount.Quantity=request.Quantity ?? discount.Quantity;
            discount.Type=request.Type;

            DiscountValidator validator = new();
            validator.ValidateAndThrowException(discount);

            _unitOfWork.Discounts.Update(discount);
            await _unitOfWork.CompleteAsync();
            return discount.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                UpdateDiscount createRequest => await Handle(createRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _unitOfWork.Discounts.Update(_originalDiscount);
            await _unitOfWork.CompleteAsync();
        }
    }
}
