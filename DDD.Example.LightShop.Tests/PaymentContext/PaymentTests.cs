using System;
using DDD.Example.LightShop.PaymentContext.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.PaymentContext
{
    [TestFixture]
    public class PaymentTests
    {
        [Test]
        public void Test_CreatePayment_for_an_Order()
        {
            var id = Guid.NewGuid();
            var payment = Payment.NewPayment(id);
            var orderId = Guid.NewGuid();
            var orderSubtotal = 43900m;
            
            payment.CreatePayment(orderId, orderSubtotal);

            payment.OrderId.Should().Be(orderId);
            payment.PayableAmount.Should().Be(orderSubtotal);
            payment.PaidAmount.Should().Be(0);
            payment.State.Should().Be(PaymentState.Unpaid);
        }
    }
}