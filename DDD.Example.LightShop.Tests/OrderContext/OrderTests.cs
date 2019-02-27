using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Example.LightShop.OrderContext;
using FluentAssertions;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.OrderContext
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void Test_Construct_Order_with_GUID()
        {
            var orderId = Guid.NewGuid();
            var order = Order.NewOrder(orderId);
            order.Id.Should().Be(orderId);
        }

        [Test]
        public void Test_CreatOrder()
        {
            var orderItems = new List<Product>
            {
                Product.NewProduct(10001, "Apple Mac Book Pro 13 inch no touch bar", 43900m)
            };
            var shippingInfo = ShippingInfo.NewShippingInfo("王小明","0988123567","忠孝東路一段100號");

            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);
 
           
            order.UncommittedEvents.Should().NotBeEmpty();
            order.UncommittedEvents.FirstOrDefault()?.AggregateRootId.Should().Be(order.Id);
        }
    }
}