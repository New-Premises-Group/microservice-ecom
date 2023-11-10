using FakeItEasy;
using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.DTOs.Item;
using IW.Models.DTOs.OrderDtos;
using IW.Models.Entities;
using IW.Services;
using MapsterMapper;

namespace Test.OrderTest.Services
{
    [TestFixture]
    public class TestCreateOrder
    {
        static CreateOrder _ValidCreateOrderInput;

        [SetUp]
        public void Setup()
        {
            _ValidCreateOrderInput = new CreateOrder()
            {
                UserId = Guid.Parse("89fc34a2-a6b3-44de-8f17-b27340b904e6"),
                Total = 10000,
                Email = "hai123@gmail.com",
                ShippingAddress = "123 Tan Phu, HCM",
                Status= ORDER_STATUS.Created
            };
        }

        [Test]
        public void GivenFullDetailShouldReturnOrderCreated()
        {
            IUnitOfWork _unitOfWork = A.Fake<IUnitOfWork>();
            IMapper _mapper = new Mapper();
            IMailService _mailService = A.Fake<IMailService>();
            IRabbitMqProducer<OrderCreatedMessage> _producer = A.Fake<IRabbitMqProducer<OrderCreatedMessage>>();
            OrderService _orderService = new OrderService(_unitOfWork, _producer, _mapper, _mailService);

            int orderId = 0;

            var res = _orderService.CreateOrder(_ValidCreateOrderInput).Result;

            A.CallTo(() => _unitOfWork.Orders.Add(A<Order>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Items.AddRange(A<IEnumerable<OrderItem>>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _producer.Send(
                A<string>.Ignored,
                A<OrderCreatedMessage>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _mailService.Send(
                A<string>.Ignored,
                A<string>.Ignored,
                A<ICollection<ItemDto>>.Ignored,
                A<decimal>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.AreEqual(orderId, res);

        }
    }

    [TestFixture]
    public class TestCreateGuestOrder
    {
        static CreateGuestOrder _ValidCreateGuestOrderInput;

        [SetUp]
        public void Setup()
        {
            _ValidCreateGuestOrderInput = new CreateGuestOrder()
            {
                Phone = "0987654321",
                Total = 10000,
                Email = "hai123@gmail.com",
                ShippingAddress = "123 Tan Phu, HCM",
                Status= ORDER_STATUS.Created,
            };
        }

        [Test]
        public void GivenFullDetailShouldReturnOrderCreated()
        {
            IUnitOfWork _unitOfWork = A.Fake<IUnitOfWork>();
            IMapper _mapper = new Mapper();
            IMailService _mailService = A.Fake<IMailService>();
            IRabbitMqProducer<OrderCreatedMessage> _producer = A.Fake<IRabbitMqProducer<OrderCreatedMessage>>();
            OrderService _orderService = new OrderService(_unitOfWork, _producer, _mapper, _mailService);

            int orderId = 0;

            var res = _orderService.CreateGuestOrder(_ValidCreateGuestOrderInput).Result;

            A.CallTo(() => _unitOfWork.Orders.Add(A<Order>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Items.AddRange(A<IEnumerable<OrderItem>>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _producer.Send(
                A<string>.Ignored,
                A<OrderCreatedMessage>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _mailService.Send(
                A<string>.Ignored,
                A<string>.Ignored,
                A<ICollection<ItemDto>>.Ignored,
                A<decimal>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.AreEqual(orderId, res);

        }
    }
    //[TestFixture]
    //public class Create
    //{
    //    Order _ValidOrder=  new Order()
    //    {
    //        CancelReason = "",
    //        Date = new DateTime(),
    //        Id = 1,
    //        ShippingAddress = "123 Tan Phu, HCM",
    //        Status = ORDER_STATUS.Confirm,
    //        Total = 10000,
    //        UserId = new Guid(),
    //    };

    //    [SetUp]
    //    public void Setup()
    //    {
    //    }

        
    //}
}
