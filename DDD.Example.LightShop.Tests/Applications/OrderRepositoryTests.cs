using System;
using System.Collections.Generic;
using DDD.Example.LightShop.Applications;
using DDD.Example.LightShop.Cores.Events;
using DDD.Example.LightShop.Cores.OrderContext;
using FluentAssertions;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.Applications
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        [Test]
        public void Test_SaveOrder_Succeed()
        {
            //Given
            var aggregateRootId = Guid.NewGuid();

            var createOrderCommand = CreateOrderCommand.Prepare(aggregateRootId,
                Product.Prepare(1001, "Apple Mac Book Pro 13 inch no touchbar", 1, 43900),
                ShippingInfo.Prepare("Jed", "0988123123", "Home address"));
            
            var order = Order.Prepare(aggregateRootId);
            order.Create(createOrderCommand);

            var orderRepository = new OrderRepository();
            
            //When
            orderRepository.Save(order);
            
            //Then
            order.UncommittedChanges().Count.Should().Be(0);
            Order actual = orderRepository.Load(order.Id);
            
            actual.Should().BeEquivalentTo(order);
        }

    }
}