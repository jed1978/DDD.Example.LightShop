using System;
using System.Collections.Generic;
using DDD.Example.LightShop.OrderContext.Applications;
using DDD.Example.LightShop.OrderContext.Domain;
using DDD.Example.LightShop.SharedKernel;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.OrderContext
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        [Test]
        public void Test_Dispatch_Events_after_Events_were_Persisted()
        {
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsIsReady();
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);

            var dispatcher = Substitute.For<IEventDispatcher>();
            var repository = new OrderRepository(dispatcher);
            repository.Save(order);
            dispatcher.Received(1).Dispatch(Arg.Any<IEnumerable<IDomainEvent>>());
        }

        [Test]
        public void Test_Order_Aggregate_can_be_Save_and_Load()
        {
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsIsReady();
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);

            var dispatcher = Substitute.For<IEventDispatcher>();
            var repository = new OrderRepository(dispatcher);
            repository.Save(order);

            var actual = repository.Load(order.Id);

            actual.Should().BeEquivalentTo(order);
        }
    }
}