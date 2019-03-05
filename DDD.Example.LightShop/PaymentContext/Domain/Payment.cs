using System;
using System.Collections.Generic;
using DDD.Example.LightShop.DomainEvents;
using DDD.Example.LightShop.DomainEvents.Payment;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.PaymentContext.Domain
{
    public class Payment : IAggregateRoot<IDomainEvent>
    {
        public Payment(Guid id)
        {
            Id = id;
            UncommittedEvents = new Queue<IDomainEvent>();
        }

        public Guid Id { get; }
        public Queue<IDomainEvent> UncommittedEvents { get; }
        public Guid OrderId { get; private set; }
        public decimal PayableAmount { get; private set; }
        public decimal PaidAmount { get; private set; }
        public PaymentState State { get; private set; }

        public void ApplyChange(IDomainEvent @event, bool isRebuild)
        {
            if (!isRebuild)
            {
                UncommittedEvents.Enqueue(@event);
            }

            if (@event is PaymentRecordCreatedEvent paymentRecordCreated)
            {
                OrderId = paymentRecordCreated.OrderId;
                PayableAmount = paymentRecordCreated.PayableAmount;
                PaidAmount = 0m;
                State = PaymentState.Unpaid;
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void CreatePayment(Guid orderId, decimal orderSubtotal)
        {
            var paymentRecordCreatedEvent = new PaymentRecordCreatedEvent(Id, orderId, orderSubtotal);
            
            ApplyChange(paymentRecordCreatedEvent, false);
        }
    }
}