using IW.Common;
using IW.Exceptions.ReadItemError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.ItemDtos;
using IW.Models.Entities;
using MapsterMapper;

namespace IW.Handlers.Items
{
    public class UpdateItemHandler : AbstractCommandHandler
    {
        private OrderItem? _originalItem;
        public UpdateItemHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(UpdateItem request)
        {
            var item = await _unitOfWork.Items.GetById(request.Id);
            if (Equals(item, null)) throw new ItemNotFoundException(request.Id);
            _originalItem = item;

            item.Name = request.Name ?? item.Name;
            item.Price = request.Price ?? item.Price;
            item.ProductId = request.ProductId ?? item.ProductId;
            item.Quantity = request.Quantity ?? item.Quantity;
            item.SKU = request.SKU;

            ItemValidator validator = new();
            validator.ValidateAndThrowException(item);

            _unitOfWork.Items.Update(item);
            await _unitOfWork.CompleteAsync();
            return item.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                UpdateItem createRequest => await Handle(createRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _unitOfWork.Items.Update(_originalItem);
            await _unitOfWork.CompleteAsync();
        }
    }
}
