using IW.Common;
using IW.Interfaces;
using IW.Interfaces.Commands;
using IW.Models;
using IW.Models.DTOs.OrderDtos;
using IW.Models.Entities;
using MapsterMapper;
using IW.Exceptions.ReadOrderError;
using IW.Interfaces.Services;

namespace IW.Handlers.Orders
{
    public class DeleteOrderHandler : AbstractCommandHandler
    {
        private Order _deletedOrder;
        public DeleteOrderHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }
        private async Task<int> Handle(DeleteOrder request)
        {
            _deletedOrder = await _unitOfWork.Orders.GetById(request.Id);
            if (Equals(_deletedOrder, null)) throw new OrderNotFoundException(request.Id);

            _unitOfWork.Orders.Remove(_deletedOrder);
            await _unitOfWork.CompleteAsync();

            return request.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                DeleteOrder delRequest => await Handle(delRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _unitOfWork.Orders.Add(_deletedOrder);
            await _unitOfWork.CompleteAsync();
        }
    }
}
