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
            var (orderItems, shippingInfo) = Given_OrderDetailsWasPrepared();
            
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);
            
            order.UncommittedEvents.Should().NotBeEmpty();
            order.UncommittedEvents.FirstOrDefault()?.AggregateRootId.Should().Be(order.Id);
        }

        [Test]
        public void Test_CommitOrderChanges()
        {
            var (orderItems, shippingInfo) = Given_OrderDetailsWasPrepared();

            var order = Given_OrderWasPrepared(orderItems, shippingInfo);

            order.Commit();
            
            order.UncommittedEvents.Should().BeEmpty();
        }

        private static Order Given_OrderWasPrepared(List<Product> orderItems, ShippingInfo shippingInfo)
        {
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);
            return order;
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