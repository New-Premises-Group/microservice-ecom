using FakeItEasy;
using IW.Common;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models;
using IW.Models.Entities;
using IW.Services;
using MapsterMapper;

namespace Test.OrderTest.Services
{
    [TestFixture]
    internal class GetOrder
    {
        Order _ValidOrder = new Order()
        {
            CancelReason = "",
            Date = new DateTime(),
            Id = 1,
            ShippingAddress = "123 Tan Phu, HCM",
            Status = ORDER_STATUS.Confirmed,
            Total = 10000,
            UserId = Guid.Parse("89fc34a2-a6b3-44de-8f17-b27340b904e6"),
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1)]
        public void GivenExistIdShouldGetOne(int orderId)
        {
            IUnitOfWork _unitOfWork = A.Fake<IUnitOfWork>();
            IMapper _mapper = new Mapper();
            IMailService _mailService = A.Fake<IMailService>();
            IRabbitMqProducer<OrderCreatedMessage> _producer = A.Fake<IRabbitMqProducer<OrderCreatedMessage>>();
            OrderService _orderService = new OrderService(_unitOfWork, _producer, _mapper, _mailService);

            A.CallTo(() => _unitOfWork.Orders.GetById(orderId)).Returns(_ValidOrder);

            var res = _orderService.GetOrder(orderId);

            A.CallTo(() => _unitOfWork.Orders.GetById(A<int>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.AreEqual(orderId, res.Result.Id);
            Assert.AreEqual(_ValidOrder.Status, res.Result.Status);
            Assert.AreEqual(_ValidOrder.Total, res.Result.Total);
            Assert.AreEqual(_ValidOrder.Items, res.Result.Items);
            Assert.AreEqual(_ValidOrder.ShippingAddress, res.Result.ShippingAddress);
            Assert.AreEqual(_ValidOrder.CancelReason, res.Result.CancelReason);
            Assert.AreEqual(_ValidOrder.Date, res.Result.Date);
        }
    }

    [TestFixture]
    internal class GetOrders
    {
        Order _ValidOrder = new Order()
        {
            CancelReason = "",
            Date = new DateTime(),
            Id = 1,
            ShippingAddress = "123 Tan Phu, HCM",
            Status = ORDER_STATUS.Confirmed,
            Total = 10000,
            UserId = Guid.Parse("89fc34a2-a6b3-44de-8f17-b27340b904e6"),
        };

        List<Order> _orderList= new List<Order>()
        {
            new Order()
        {
            CancelReason = "",
            Date = new DateTime(),
            Id = 1,
            ShippingAddress = "123 Tan Phu, HCM",
            Status = ORDER_STATUS.Confirmed,
            Total = 10000,
            UserId = Guid.Parse("89fc34a2-a6b3-44de-8f17-b27340b904e6"),
        },
            new Order()
        {
            CancelReason = "",
            Date = new DateTime(),
            Id = 2,
            ShippingAddress = "456 Tan Phu, HCM",
            Status = ORDER_STATUS.Confirmed,
            Total = 20000,
            UserId = Guid.Parse("89fc34a2-a6b3-44de-8f17-b27340b904e6"),
        }
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenNoQueryShouldOrderList()
        {
            IUnitOfWork _unitOfWork = A.Fake<IUnitOfWork>();
            IMapper _mapper = new Mapper();
            IMailService _mailService = A.Fake<IMailService>();
            IRabbitMqProducer<OrderCreatedMessage> _producer = A.Fake<IRabbitMqProducer<OrderCreatedMessage>>();
            OrderService _orderService = new OrderService(_unitOfWork, _producer, _mapper, _mailService);

            A.CallTo(() => _unitOfWork.Orders.GetAll(A<int>.Ignored, A<int>.Ignored)).Returns(_orderList);

            var res = _orderService.GetOrders((int)PAGINATING.OffsetDefault,(int)PAGINATING.AmountDefault);

            A.CallTo(() => _unitOfWork.Orders.GetAll(A<int>.Ignored, A<int>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.AreEqual(_orderList.Count, res.Result.Count());
            //Assert.AreEqual(_ValidOrder.Status, res.Result.Status);
            //Assert.AreEqual(_ValidOrder.Total, res.Result.Total);
            //Assert.AreEqual(_ValidOrder.Items, res.Result.Items);
            //Assert.AreEqual(_ValidOrder.ShippingAddress, res.Result.ShippingAddress);
            //Assert.AreEqual(_ValidOrder.CancelReason, res.Result.CancelReason);
            //Assert.AreEqual(_ValidOrder.Date, res.Result.Date);
        }
    }
}
