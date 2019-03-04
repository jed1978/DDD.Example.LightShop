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
            order.Subtotal.Should().Be(0);
        }

        [Test]
        public void Test_CreatOrder()
        {
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsIsReady();

            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);

            order.UncommittedEvents.Count.Should().Be(3);
            order.UncommittedEvents.FirstOrDefault()?.AggregateRootId.Should().Be(order.Id);
            order.OrderItems.Should().BeEquivalentTo(orderItems);
            order.ShippingInfo.Should().BeEquivalentTo(shippingInfo);
            order.Subtotal.Should().Be(orderItems.Sum(p => p.UnitPrice));
        }

        [Test]
        public void Test_CommitOrderChanges()
        {
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsIsReady();

            var order = OrderTestHelper.Given_OrderIsReady(orderItems, shippingInfo);

            order.Commit();

            order.UncommittedEvents.Should().BeEmpty();
            order.Subtotal.Should().Be(orderItems.Sum(p => p.UnitPrice));
        }
    }
}