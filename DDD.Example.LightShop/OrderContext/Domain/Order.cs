using System;
using System.Collections.Generic;
using DDD.Example.LightShop.OrderContext.DomainEvents;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.OrderContext.Domain
{
    public class Order : IAggregateRoot
    {
        public static Order NewOrder(Guid Id)
        {
            return new Order(Id);
        }

        private Order(Guid Id)
        {
            this.Id = Id;
            UncommittedEvents = new Queue<OrderCreatedEvent>();
        }

        public Guid Id { get; private set; }

        public Queue<OrderCreatedEvent> UncommittedEvents { get; }

        public void Create(List<Product> orderItems, ShippingInfo shippingInfo)
        {
            ApplyChange(OrderCreatedEvent.NewOrderCreatedEvent(Id, orderItems, shippingInfo));
        }

        private void ApplyChange(OrderCreatedEvent @event)
        {
            UncommittedEvents.Enqueue(@event);
        }

        public void Commit()
        {
            UncommittedEvents.Clear();
        }
    }
}