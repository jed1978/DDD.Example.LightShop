using System;
using System.Collections.Generic;
using DDD.Example.LightShop.Infrastructure;
using DDD.Example.LightShop.OrderContext.Application;
using DDD.Example.LightShop.OrderContext.Domain;
using DDD.Example.LightShop.SharedKernel;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.OrderContext
{
    [TestFixture]
    public class OrderRepositoryTests 
    {
        [Test]
        public void Test_Save_and_Load_Order()
        {
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsIsReady();
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);
            
            var dispatcher = Substitute.For<IEventDispatcher>();
            var repository = new OrderRepository(dispatcher);
            repository.Save(order);

            Order actual = repository.Load(order.Id);
            
            actual.Should().BeEquivalentTo(order);
        }

        [Test]
        public void Test_Save_and_DispatchEvent()
        {
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsIsReady();
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);

            var dispatcher = Substitute.For<IEventDispatcher>();
            var repository = new OrderRepository(dispatcher);
            repository.Save(order);
            dispatcher.Received(1).Dispatch(Arg.Any<IEnumerable<IDomainEvent>>());
        }
    }
}