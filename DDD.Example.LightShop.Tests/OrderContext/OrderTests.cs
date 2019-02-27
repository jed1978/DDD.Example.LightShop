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
            var order = new Order(orderId);
            order.Id.Should().Be(orderId);
        }

        [Test]
        public void Test_CreatOrder()
        {
            var orderId = Guid.NewGuid();
            var orderItems = new List<Product>();
            orderItems.Add(new Product
            {
                Id = 10001,
                ItemName = "Apple Mac Book Pro 13 inch no touch bar",
                UnitPrice = 43900m
            });
            var shippingInfo = new ShippingInfo
            {
                ContactName = "王小明",
                ContactPhone = "0988123567",
                ShippingAddress = "忠孝東路一段100號"
            };
            
            var order = new Order(orderId);
            order.Create(orderItems, shippingInfo);

            order.UncommittedEvents.Should().NotBeEmpty();
            order.UncommittedEvents.FirstOrDefault()?.AggregateRootId.Should().Be(order.Id);
        }
    }
}