using IW.Common;
using IW.Interfaces;
using IW.Interfaces.Commands;
using IW.Models.DTOs.Item;
using IW.Models;
using IW.Models.DTOs.ItemDtos;
using IW.Models.DTOs.OrderDtos;
using IW.Models.Entities;
using MapsterMapper;
using IW.Exceptions.ReadItemError;
using IW.Interfaces.Services;

namespace IW.Handlers.Items
{
    public class DeleteItemHandler : AbstractCommandHandler
    {
        private OrderItem? _savedItems;
        public DeleteItemHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(DeleteItem request)
        {
            var item = await _unitOfWork.Items.GetById(request.Id);
            if (Equals(item, null)) throw new ItemNotFoundException(request.Id);

            _unitOfWork.Items.Remove(item);
            await _unitOfWork.CompleteAsync();
            return request.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                DeleteItem createRequest => await Handle(createRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _unitOfWork.Items.Add(_savedItems);
            await _unitOfWork.CompleteAsync();
        }
    }
}
