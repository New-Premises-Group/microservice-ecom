using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.DTOs.OrderDtos;
using IW.Models.Entities;
using MapsterMapper;
using IW.Models.DTOs;
using IW.Models.DTOs.ItemDtos;
using IW.Interfaces.Services;
using IW.Models.DTOs.DiscountDtos;

namespace IW.Handlers.Orders
{
    public class CreateOrderHandler : AbstractCommandHandler
    {

        private Order? _savedOrder;
        public CreateOrderHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        private async Task<int> Handle(CreateOrder request)
        {
            Order newOrder = _mapper.Map<Order>(request);
            newOrder.Date = DateTime.Now.ToUniversalTime();
            newOrder.Status = request.Status;

            OrderValidator validator = new();
            validator.ValidateAndThrowException(newOrder);

            await _mediator.Send(new ApplyDiscount()
            {
                Code = newOrder.DiscountCode,
                Order = newOrder,
                Condition = request.Condition,
            });

            _unitOfWork.Orders.Add(newOrder);

            await _unitOfWork.CompleteAsync();

            var createNotification = _mapper.Map<CreateNotification>(newOrder);
            createNotification.Message = _mapper.Map<OrderCreatedMessage>(newOrder);

            //Task createItem= _mediator.Send(_mapper.Map<CreateItems>(newOrder));
            Task createNoti = _mediator.Send(createNotification);

            _savedOrder = newOrder;
            await Task.WhenAll(
                //createItem,
                createNoti);
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
