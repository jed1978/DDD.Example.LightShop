using DDD.Example.LightShop.OrderContext;
using DDD.Example.LightShop.OrderContext.Application;
using DDD.Example.LightShop.OrderContext.Domain;
using DDD.Example.LightShop.SharedKernel;
using NSubstitute;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.OrderContext
{
    [TestFixture]
    public class OrderApplicationServiceTests
    {
        [Test]
        public void Test_CreateOrder_and_SaveToRepository()
        {
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsIsReady();

            var orderRepository = Substitute.For<IRepository>();
            var service = new OrderApplicationService(orderRepository);

            service.CreateOrder(orderItems, shippingInfo);

            orderRepository.Received(1).Save(Arg.Any<Order>());
        }
    }
}