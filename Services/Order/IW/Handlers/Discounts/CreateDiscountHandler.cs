using IW.Common;
using IW.Interfaces;
using IW.Models.Entities;
using MapsterMapper;
using IW.Models.DTOs.ItemDtos;
using IW.Interfaces.Services;
using IW.Models.DTOs.DiscountDtos;

namespace IW.Handlers.Discounts
{
    public class CreateDiscountHandler : AbstractCommandHandler
    {
        private Discount? _savedDiscount;
        public CreateDiscountHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(CreateDiscount request)
        {
            Discount newDiscount = _mapper.Map<Discount>(request);
            DiscountValidator validator = new();

            validator.ValidateAndThrowException(newDiscount);

            _savedDiscount = newDiscount;
            _unitOfWork.Discounts.Add(newDiscount);
            await _unitOfWork.CompleteAsync();
            return newDiscount.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                CreateDiscount createRequest => await Handle(createRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _unitOfWork.Discounts.Remove(_savedDiscount);
            await _unitOfWork.CompleteAsync();
        }
    }
}
