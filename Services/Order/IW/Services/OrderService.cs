using IW.Common;
using IW.MessageBroker;
using IW.Exceptions.ReadOrderError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.OrderDto;
using IW.Models.Entities;
using MapsterMapper;
using Mapster;
using IW.Models.DTOs.Item;
using IW.Models;

namespace IW.Services
{
    public class OrderService : IOrderService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IRabbitMqProducer<OrderCreatedMessage> _producer;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IRabbitMqProducer<OrderCreatedMessage> producer,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _producer = producer;
            _mapper = mapper;
        }

        public async Task<int> CreateOrder(CreateOrder input)
        {
            Order newOrder = _mapper.Map<Order>(input);
            newOrder.Date = DateTime.Now.ToUniversalTime();
            newOrder.Status = ORDER_STATUS.Confirm;

            OrderValidator validator = new();
            validator.ValidateAndThrowException(newOrder);

            _unitOfWork.Orders.Add(newOrder);
            _unitOfWork.Items.AddRange(newOrder.Items);
            var message = newOrder.Adapt<OrderCreatedMessage>();
            await _unitOfWork.CompleteAsync();
            _producer.Send(nameof(QUEUE_NAME.Order_Placed),message);
            return newOrder.Id;
        }

        public async Task DeleteOrder(int id)
        {
            var order = await OrderExist(id);
            if (Equals(order, null)) throw new OrderNotFoundException(id);

            _unitOfWork.Orders.Remove(order);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(int offset,int amount )
        {
            var orders = await _unitOfWork.Orders.GetAll(offset,amount);
            ICollection<OrderDto> result = new List<OrderDto>();
            foreach (var order in orders)
            {
                OrderDto item = new()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    Status = order.Status,
                    ShippingAddress= order.ShippingAddress,
                    Date= order.Date,
                    Items = order.Items
                    
                };
                result.Add(item);
            }
            return result;
        }

        public async Task<OrderDto> GetOrder(int id)
        {
            var order = await OrderExist(id);
            if (Equals(order, null))
            {
                throw new OrderNotFoundException(id);
            }
            OrderDto result = new()
            {
                Id = id,
                Date = order.Date,
                Items= order.Items,
                ShippingAddress = order.ShippingAddress,
                Status = order.Status,
                UserId = order.UserId
            };
            return result;
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(GetOrder query, int offset, int amount)
        {
            var orders = await _unitOfWork.Orders.FindByConditionToList(
                o => o.UserId==query.UserId ||
                o.Status==query.Status ||
                o.Date ==query.Date
                , offset, amount);

            ICollection<OrderDto> result = new List<OrderDto>();
            foreach (var order in orders)
            {
                OrderDto item = new()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    Status = order.Status,
                    ShippingAddress = order.ShippingAddress,
                    Date = order.Date,
                    Items = order.Items

                };
                result.Add(item);
            }
            return result;
        }

        public async Task UpdateOrder(int id, UpdateOrder input)
        {
            var order = await OrderExist(id);
            if (Equals(order, null)) throw new OrderNotFoundException(id);

            order.Status = input.Status;
            order.ShippingAddress = input.ShippingAddress ?? order.ShippingAddress;

            OrderValidator validator = new ();
            validator.ValidateAndThrowException(order);

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<Order?> OrderExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var Order = await _unitOfWork.Orders.GetById(id);
            return Order;
        }
    }
}
