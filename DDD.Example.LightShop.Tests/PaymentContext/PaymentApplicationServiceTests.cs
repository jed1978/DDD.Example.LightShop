using DDD.Example.LightShop.DomainEvents;
using DDD.Example.LightShop.PaymentContext.Applications;
using DDD.Example.LightShop.SharedKernel;
using NSubstitute;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.PaymentContext
{
    [TestFixture]
    public class PaymentApplicationServiceTests
    {
        [Test]
        public void Test_Register_EventHandler_to_EventDispatcher()
        {
            var dispatcher = Substitute.For<IEventDispatcher>();
            var handler = Substitute.For<IEventHandler<IDomainEvent>>();
            var service = new PaymentApplicationService(dispatcher);

            service.Register(handler);

            dispatcher.Received(1).Register<OrderCreatedEvent>(Arg.Any<IEventHandler<IDomainEvent>>());
        }
    }
}