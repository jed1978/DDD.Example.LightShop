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

        public Guid EventId { get; }
        public Guid AggregateRootId { get; }
        public Guid OrderId { get; }
        public decimal PayableAmount { get; }
        public DateTime OccuredOn { get; }
    }
}