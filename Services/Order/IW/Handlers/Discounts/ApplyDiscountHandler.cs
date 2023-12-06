using IW.Common;
using IW.Interfaces;
using IW.Models.Entities;
using MapsterMapper;
using IW.Interfaces.Services;
using IW.Models.DTOs.DiscountDtos;

namespace IW.Handlers.Discounts
{
    public class ApplyDiscountHandler : AbstractCommandHandler
    {
        private Discount? _savedDiscount;
        public ApplyDiscountHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(ApplyDiscount request)
        {
            Discount? discount = await _unitOfWork
                .Discounts
                .FindByCondition(
                discount => discount.Code == request.Code);

            discount ??= Discount.Empty;

            _savedDiscount = discount;
            request.Order.Total=discount.Apply(request.Order.Total,request.Condition);

            return discount.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                ApplyDiscount createRequest => await Handle(createRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            //_unitOfWork.Discounts.Remove(_savedDiscount);
            await _unitOfWork.CompleteAsync();
        }
    }
}
