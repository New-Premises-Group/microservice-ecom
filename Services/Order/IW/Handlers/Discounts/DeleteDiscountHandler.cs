using IW.Common;
using IW.Interfaces;
using IW.Models.DTOs.ItemDtos;
using IW.Models.Entities;
using MapsterMapper;
using IW.Exceptions.ReadItemError;
using IW.Interfaces.Services;
using IW.Models.DTOs.DiscountDtos;

namespace IW.Handlers.Discounts
{
    public class DeleteDiscountHandler : AbstractCommandHandler
    {
        private Discount? _savedDiscount;
        public DeleteDiscountHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(DeleteDiscount request)
        {
            var discount = await _unitOfWork.Discounts.GetById(request.Id);
            if (Equals(discount, null)) throw new ItemNotFoundException(request.Id);

            _unitOfWork.Discounts.Remove(discount);
            await _unitOfWork.CompleteAsync();
            return request.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                DeleteDiscount createRequest => await Handle(createRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _unitOfWork.Discounts.Add(_savedDiscount);
            await _unitOfWork.CompleteAsync();
        }
    }
}
