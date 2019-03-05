using System;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.DomainEvents.Order
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public Guid EventId { get; private set; }

        public Guid AggregateRootId { get; private set;}

        public DateTime OccuredOn { get; private set;}

        public decimal Subtotal { get; private set;}

        private OrderCreatedEvent(Guid orderId, decimal subtotal)
        {
            EventId = Guid.NewGuid();
            AggregateRootId = orderId;
            Subtotal = subtotal;
            OccuredOn = DateTime.Now;
        }

        public static OrderCreatedEvent Raise(Guid orderId, decimal subtotal)
        {
            return new OrderCreatedEvent(orderId, subtotal);
        }
    }
}