using DDD.Example.LightShop.DomainEvents;
using DDD.Example.LightShop.DomainEvents.Order;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.PaymentContext.Applications
{
    public class PaymentApplicationService
    {
        private readonly IEventDispatcher _eventDispatcher;

        public PaymentApplicationService(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public void Register(IEventHandler<IDomainEvent> handler)
        {
            _eventDispatcher.Register<OrderCreatedEvent>(handler);
        }
    }
}