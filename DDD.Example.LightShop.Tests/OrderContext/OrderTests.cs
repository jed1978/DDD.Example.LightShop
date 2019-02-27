using System;
using DDD.Example.LightShop.OrderContext;
using FluentAssertions;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.OrderContext
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void Test_CreateEmptyOrder()
        {
            var orderId = Guid.NewGuid();
            var order = new Order(orderId);
            order.Id.Should().Be(orderId);
        }
    }
}