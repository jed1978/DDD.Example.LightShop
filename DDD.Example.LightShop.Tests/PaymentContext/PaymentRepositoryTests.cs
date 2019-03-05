using System;
using DDD.Example.LightShop.PaymentContext.Applications;
using DDD.Example.LightShop.PaymentContext.Domain;
using DDD.Example.LightShop.SharedKernel;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.PaymentContext
{
    [TestFixture]
    public class PaymentRepositoryTests
    {
        [Test]
        public void Test_Payment_Aggregate_can_be_Save_and_Load()
        {
            var dispatcher = Substitute.For<IEventDispatcher>();
            var repository = new PaymentRepository(dispatcher);

            var paymentId = Guid.NewGuid();
            var orderId = Guid.NewGuid();
            var orderSubtotal = 43900;
            
            var payment = Payment.NewPayment(paymentId);
            
            payment.CreatePayment(orderId, orderSubtotal);
            
            repository.Save(payment);
            var actual = repository.Load(paymentId);
            
            actual.Should().BeEquivalentTo(payment);
        }
    }
}