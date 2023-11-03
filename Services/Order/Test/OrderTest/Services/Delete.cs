using FakeItEasy;
using IW.Interfaces;
using IW.Models;
using IW.Models.Entities;
using IW.Services;
using MapsterMapper;

namespace Test.OrderTest.Services
{
    [TestFixture]
    internal class DeleteOrdedr
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        [TestCase(0)]
        public void GivenExistIdShouldDelete(int orderId)
        {
            IUnitOfWork _unitOfWork = A.Fake<IUnitOfWork>();
            IMapper _mapper = new Mapper();
            IMailService _mailService = A.Fake<IMailService>();
            IRabbitMqProducer<OrderCreatedMessage> _producer = A.Fake<IRabbitMqProducer<OrderCreatedMessage>>();
            OrderService _orderService = new OrderService(_unitOfWork, _producer, _mapper, _mailService);
            _orderService.DeleteOrder(orderId).Wait();

            A.CallTo(() => _unitOfWork.Orders.GetById(orderId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.Orders.Remove(A<Order>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
