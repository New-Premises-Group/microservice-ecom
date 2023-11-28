using IW.Common;
using IW.Interfaces;
using IW.Interfaces.Commands;
using IW.Models.DTOs.Item;
using IW.Models;
using IW.Models.DTOs.OrderDtos;
using IW.Models.Entities;
using IW.Notifications;
using MapsterMapper;
using IW.Exceptions.ReadOrderError;
using IW.Interfaces.Services;

namespace IW.Handlers.Orders
{
    public class FinishOrderHandler : AbstractCommandHandler
    {
        private Order? _orignalOrder;
        private ORDER_STATUS _lastStatus;
        public FinishOrderHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(FinishOrder request)
        {
            _orignalOrder = await _unitOfWork.Orders.GetById(request.Id);
            if (Equals(_orignalOrder, null)) throw new OrderNotFoundException(request.Id);

            _lastStatus = _orignalOrder.Status;
            await _unitOfWork.Orders.SetDone(request.Id);

            return request.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                FinishOrder finishRequest => await Handle(finishRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _orignalOrder.Status = _lastStatus;
            _unitOfWork.Orders.Update(_orignalOrder);
            await _unitOfWork.CompleteAsync();
        }
    }
}
