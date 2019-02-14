using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DDD.Example.LightShop.Cores;
using DDD.Example.LightShop.Cores.Events;
using DDD.Example.LightShop.Cores.OrderDomain;
using FluentAssertions;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.Cores
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void CreateOrder_MBP13_Succeed()
        {
            //Given
            var aggregateRootId = Guid.NewGuid();

            var product = Product.Prepare(1001, "Apple Mac Book Pro 13 inch no touchbar", 1, 43900);
            var shippingInfo = ShippingInfo.Prepare("Jed", "0988123123", "Home address");

            var createOrderCommand = CreateOrderCommand.Prepare(aggregateRootId, product, shippingInfo);

            var order = Order.Prepare(aggregateRootId);

            var expected = new List<OrderCreatedEvent>
            {
                OrderCreatedEvent.Prepare(aggregateRootId, product, shippingInfo)
            };

            //When
            order.Create(createOrderCommand);
            var changes = order.UncommittedChanges();

            //Then
            changes.Should().BeEquivalentTo(expected);
            order.UncommittedChanges().Count.Should().Be(1);
        }

        [Test]
        public void RebuildOrderFromHistory()
        {
            var aggregateRootId = Guid.NewGuid();

            var historicalEvents = new List<OrderCreatedEvent>
            {
                OrderCreatedEvent.Prepare(aggregateRootId,
                    Product.Prepare(1001, "Apple Mac Book Pro 13 inch no touchbar", 1, 43900),
                    ShippingInfo.Prepare("Jed", "0988123123", "Home address"))
            };

            var order = Order.Prepare(aggregateRootId);

            order.Rebuild(historicalEvents);


            order.UncommittedChanges().Count.Should().Be(0);
            order.Product.Should().BeEquivalentTo(historicalEvents.First().Product);
            order.ShippingInfo.Should().BeEquivalentTo(historicalEvents.First().ShippingInfo);
        }
    }
}