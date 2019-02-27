using System;
using System.Collections.Generic;
using DDD.Example.LightShop.OrderContext.Application;
using DDD.Example.LightShop.OrderContext.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.OrderContext
{
    [TestFixture]
    public class OrderRepositoryTests 
    {
        [Test]
        public void Test_Save_and_Load_Order()
        {
            var (orderItems, shippingInfo) = OrderTestHelper.Given_OrderDetailsWasPrepared();
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);
            
            var repository = new OrderRepository();
            repository.Save(order);

            Order actual = repository.Load(order.Id);
            
            actual.Should().BeEquivalentTo(order);
        }
    }
}