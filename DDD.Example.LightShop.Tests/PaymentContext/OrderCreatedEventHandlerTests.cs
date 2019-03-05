using System;
using DDD.Example.LightShop.DomainEvents;
using DDD.Example.LightShop.DomainEvents.Order;
using DDD.Example.LightShop.PaymentContext.Applications;
using DDD.Example.LightShop.PaymentContext.Domain;
using DDD.Example.LightShop.SharedKernel;
using NSubstitute;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.PaymentContext
{
    [TestFixture]
    public class OrderCreatedEventHandlerTests
    {
        [Test]
        public void Test_Should_Create_PaymentRecord_when_Handle_OrderCreatedEvent_()
        {
            var repository = Substitute.For<IRepository<Payment>>();
            var handler = new OrderCreatedEventHandler(repository);

            handler.Handle(OrderCreatedEvent.Raise(Guid.NewGuid(), 43900));
            
            repository.Received(1).Save(Arg.Any<Payment>());
        }
    }
}