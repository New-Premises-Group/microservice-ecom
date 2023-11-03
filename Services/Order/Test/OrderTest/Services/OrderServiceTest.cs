using FakeItEasy;
using IW.Common;
using IW.Interfaces;
using IW.Models;
using IW.Models.DTOs.OrderDto;
using IW.Models.Entities;
using IW.Services;
using MapsterMapper;

namespace Test.OrderTest.Services
{
    [TestFixture]
    internal class OrderServiceTest
    {
        Order _ValidOrder;
        CreateOrder _ValidCreateOrderInput;

        [SetUp]
        public void Setup()
        {
            _ValidOrder = new Order()
            {
                CancelReason = "",
                Date = new DateTime(),
                Id = 1,
                ShippingAddress = "123 Tan Phu, HCM",
                Status = ORDER_STATUS.Confirmed,
                Total = 10000,
                UserId = new Guid(),
            };
            _ValidCreateOrderInput = new CreateOrder()
            {
                UserId = Guid.Parse("89fc34a2-a6b3-44de-8f17-b27340b904e6"),
                Total = 10000,
                Email = "hai123@gmail.com",
                ShippingAddress = "123 Tan Phu, HCM"
            };


        }
    }
}
