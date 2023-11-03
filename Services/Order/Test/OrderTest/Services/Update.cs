using FakeItEasy;
using IW.Interfaces;
using IW.Models.DTOs.OrderDto;
using IW.Models;
using IW.Services;
using MapsterMapper;
using IW.Models.DTOs.Item;
using IW.Common;
using IW.Models.Entities;
using FakeItEasy.Sdk;
using IW.Interfaces.Repositories;

namespace Test.OrderTest.Services
{
    [TestFixture]
    internal class Update
    {
        Order _ValidOrder;

        static CreateOrder _ValidCreateOrderInput;
        UpdateOrder _ValidUpdateOrderInput;

        [SetUp]
        public void Setup()
        {
            _ValidCreateOrderInput = new CreateOrder()
            {
                UserId = Guid.Parse("89fc34a2-a6b3-44de-8f17-b27340b904e6"),
                Total = 10000,
                Email = "hai123@gmail.com",
                ShippingAddress = "123 Tan Phu, HCM"
            };

            _ValidOrder = new Order()
            {
                CancelReason = "",
                Date = DateTime.Now,
                Id = 0,
                ShippingAddress = "123 Tan Phu, HCM",
                Status = ORDER_STATUS.Confirm,
                Total = 10000,
                UserId = Guid.Parse("89fc34a2-a6b3-44de-8f17-b27340b904e6"),
            };

            _ValidUpdateOrderInput = new UpdateOrder()
            {
                CancelReason="",
                ShippingAddress="123 Tan Phu",
                Status = ORDER_STATUS.Confirm,
            };
        }

        [Test]
        public void GivenFullDetailShouldReturnOrderUpdated()
        {
            IUnitOfWork _unitOfWork = A.Fake<IUnitOfWork>();

            IMapper _mapper = new Mapper();
            IMailService _mailService = A.Fake<IMailService>();
            IRabbitMqProducer<OrderCreatedMessage> _producer = A.Fake
                <IRabbitMqProducer<OrderCreatedMessage>>();

            OrderService _orderService = new OrderService(_unitOfWork, _producer, _mapper, _mailService);

            int orderId = 0;
            A.CallTo(() => _unitOfWork.Orders.GetById(orderId)).Returns(_ValidOrder);

            var res = _orderService.UpdateOrder(orderId, _ValidUpdateOrderInput);

            A.CallTo(() => _unitOfWork.Orders.GetById(orderId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Orders.Update(A<Order>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();

        }
    }
}
