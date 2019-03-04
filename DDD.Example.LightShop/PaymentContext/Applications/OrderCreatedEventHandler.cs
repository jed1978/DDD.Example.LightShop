using System;
using DDD.Example.LightShop.DomainEvents;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.PaymentContext.Applications
{
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly IRepository _repository;

        public OrderCreatedEventHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(OrderCreatedEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}