using System;
using System.Collections.Generic;
using DDD.Example.LightShop.OrderContext.DomainEvents;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.OrderContext.Domain
{
    public class Order : IAggregateRoot<IDomainEvent>
    {

        public Guid Id { get; private set; }

        public Queue<IDomainEvent> UncommittedEvents { get; }
        

        private Order(Guid Id)
        {
            this.Id = Id;
            UncommittedEvents = new Queue<IDomainEvent>();
        }
        
        public static Order NewOrder(Guid Id)
        {
            return new Order(Id);
        }

        public void Create(List<Product> orderItems, ShippingInfo shippingInfo)
        {
            ApplyChange(OrderCreatedEvent.NewOrderCreatedEvent(Id, orderItems, shippingInfo));
        }

        public void ApplyChange(IDomainEvent @event)
        {
            UncommittedEvents.Enqueue(@event);
        }

        public void Commit()
        {
            UncommittedEvents.Clear();
        }
    }
}