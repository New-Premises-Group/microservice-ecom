using IW.Common;
using IW.Interfaces;
using IW.Interfaces.Commands;
using IW.Models.DTOs.Item;
using IW.Models;
using IW.Models.DTOs.OrderDtos;
using IW.Models.Entities;
using MapsterMapper;
using IW.Exceptions.ReadOrderError;
using IW.Commands;
using IW.Models.DTOs;
using IW.Models.DTOs.ItemDtos;
using IW.Interfaces.Services;

namespace IW.Handlers.Orders
{
    public class CreateOrderHandler : AbstractCommandHandler
    {
        
        private Order? _savedOrder;
        public CreateOrderHandler(IUnitOfWork unitOfWork,
            IMapper mapper, 
            IMediator mediator) : base(unitOfWork, mapper,mediator)
        {
        }

        private async Task<int> Handle(CreateOrder request)
        {
            Order newOrder = _mapper.Map<Order>(request);
            newOrder.Date = DateTime.Now.ToUniversalTime();
            newOrder.Status = request.Status;

            OrderValidator validator = new();
            validator.ValidateAndThrowException(newOrder);

            _unitOfWork.Orders.Add(newOrder);
            
            await _unitOfWork.CompleteAsync();

            var createNotification = _mapper.Map<CreateNotification>(newOrder);
            createNotification.Message = _mapper.Map<OrderCreatedMessage>(newOrder);
            await _mediator.Send(_mapper.Map<CreateItems>(newOrder));
            await _mediator.Send(createNotification);

            _savedOrder = newOrder;
            return newOrder.Id;
        }

        public override async Task<int> Handle<TRequest>(TRequest request)
        {
            return request switch
            {
                CreateOrder createRequest => await Handle(createRequest),
                _ => throw new ArgumentException(),
            };
        }

        public override async Task Undo()
        {
            _unitOfWork.Orders.Remove(_savedOrder);
            await _unitOfWork.CompleteAsync();
        }
    }
}
