using System.Collections.Generic;
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
            var (orderItems, shippingInfo) = Given_OrderDetailsWasPrepared();

            var eventStoreRepository = Substitute.For<IRepository>();
            var service = new OrderApplicationService(eventStoreRepository);

            service.CreateOrder(orderItems, shippingInfo);

            eventStoreRepository.Received(1).Save(Arg.Any<Order>());
        }

        private static (List<Product> orderItems, ShippingInfo shippingInfo) Given_OrderDetailsWasPrepared()
        {
            var orderItems = new List<Product>
            {
                Product.NewProduct(10001, "Apple Mac Book Pro 13 inch no touch bar", 43900m)
            };
            var shippingInfo = ShippingInfo.NewShippingInfo("王小明", "0988123567", "忠孝東路一段100號");
            return (orderItems, shippingInfo);
        }
    }
}