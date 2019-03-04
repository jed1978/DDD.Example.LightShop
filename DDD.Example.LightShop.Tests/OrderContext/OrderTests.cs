using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Example.LightShop.OrderContext;
using DDD.Example.LightShop.OrderContext.Domain;
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
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsIsReady();
            
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);
            
            order.UncommittedEvents.Should().NotBeEmpty();
            order.UncommittedEvents.FirstOrDefault()?.AggregateRootId.Should().Be(order.Id);
        }

        [Test]
        public void Test_CommitOrderChanges()
        {
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsIsReady();

            var order = OrderTestHelper.Given_OrderIsReady(orderItems, shippingInfo);

            order.Commit();
            
            order.UncommittedEvents.Should().BeEmpty();
        }
    }
}