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
    public class UpdateOrderHandler : AbstractCommandHandler
    {
        private Order? orignalOrder;
        public UpdateOrderHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(UpdateOrder request)
        {
            var updatedOrder = await _unitOfWork.Orders.GetById(request.Id);

            if (Equals(updatedOrder, null)) 
                throw new OrderNotFoundException(request.Id);

            orignalOrder = updatedOrder;

            updatedOrder.CancelReason = request.CancelReason; 
            updatedOrder.UserName = request.UserName;
            updatedOrder.Phone = request.Phone;

            updatedOrder.Status = 
                request.Status ?? updatedOrder.Status;
            
            updatedOrder.ShippingAddress = 
                request.ShippingAddress ?? 
                updatedOrder.ShippingAddress;

            OrderValidator validator = new();
            validator.ValidateAndThrowException(updatedOrder);

            _unitOfWork.Orders.Update(updatedOrder);
            await _unitOfWork.CompleteAsync();

            return updatedOrder.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                UpdateOrder updateRequest => await Handle(updateRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _unitOfWork.Orders.Update(orignalOrder);
            await _unitOfWork.CompleteAsync();
        }
    }
}
