using System;
using DDD.Example.LightShop.DomainEvents;
using DDD.Example.LightShop.PaymentContext.Domain;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.PaymentContext.Applications
{
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly IRepository<Payment> _repository;

        public OrderCreatedEventHandler(IRepository<Payment> repository)
        {
            _repository = repository;
        }

        public void Handle(OrderCreatedEvent domainEvent)
        {
            var payment = new Payment(Guid.NewGuid());

            _repository.Save(payment);
        }
    }
}