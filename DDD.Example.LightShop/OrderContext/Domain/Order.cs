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

        public void Commit()
        {
            UncommittedEvents.Clear();
        }

        public void Rebuild(IEnumerable<OrderCreatedEvent> historicalEvents)
        {
            foreach (var @event in historicalEvents)
            {
                ApplyChange(@event, true);
            }
        }

        public void ApplyChange(IDomainEvent @event, bool isRebuild = false)
        {
            if (!isRebuild)
            {
                UncommittedEvents.Enqueue(@event);
            }
        }
    }
}