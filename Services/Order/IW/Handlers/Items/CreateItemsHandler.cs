using IW.Common;
using IW.Interfaces;
using IW.Models.Entities;
using IW.Models;
using MapsterMapper;
using IW.Models.DTOs.ItemDtos;
using IW.Interfaces.Services;

namespace IW.Handlers.Items
{
    public class CreateItemsHandler : AbstractCommandHandler
    {
        private ICollection<OrderItem>? _savedItems;
        public CreateItemsHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(CreateItems request)
        {
            ICollection<OrderItem> items = new List<OrderItem>();
            ItemValidator validator = new();

            foreach (var input in request.Items)
            {
                OrderItem newProduct = _mapper.Map<OrderItem>(input);
                newProduct.OrderId = request.OrderId;

                validator.ValidateAndThrowException(newProduct);
                items.Add(newProduct);
            }

            _savedItems = items;

            _unitOfWork.Items.AddRange(items);
            await _unitOfWork.CompleteAsync();
            return request.OrderId;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                CreateItems createRequest => await Handle(createRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _unitOfWork.Items.RemoveRange(_savedItems);
            await _unitOfWork.CompleteAsync();
        }
    }
}
