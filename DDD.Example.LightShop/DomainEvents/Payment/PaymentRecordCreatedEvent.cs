using System;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.DomainEvents.Payment
{
    public class PaymentRecordCreatedEvent : IDomainEvent
    {
        public PaymentRecordCreatedEvent(Guid paymentId, Guid orderId, decimal orderSubtotal)
        {
            EventId = Guid.NewGuid();
            OccuredOn = DateTime.Now;
            AggregateRootId = paymentId;
            OrderId = orderId;
            PayableAmount = orderSubtotal;
        }

        public Guid EventId { get; private set;}
        public Guid AggregateRootId { get; private set;}
        public Guid OrderId { get; private set;}
        public decimal PayableAmount { get; private set;}
        public DateTime OccuredOn { get; private set;}
    }
}