using System;
using DDD.Example.LightShop.DomainEvents;
using DDD.Example.LightShop.DomainEvents.Payment;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.PaymentContext.Domain
{
    public class Payment : EntityBase, IAggregateRoot<IDomainEvent>
    {
        public Guid OrderId { get; private set; }
        public decimal PayableAmount { get; private set; }
        public decimal PaidAmount { get; private set; }
        public PaymentState State { get; private set; }

        public static Payment NewPayment(Guid id)
        {
            return new Payment(id);
        }

        private Payment(Guid id) : base(id)
        {
        }
        
        public void CreatePayment(Guid orderId, decimal orderSubtotal)
        {
            var paymentRecordCreatedEvent = new PaymentRecordCreatedEvent(Id, orderId, orderSubtotal);
            
            ApplyChange(paymentRecordCreatedEvent, false);
        }

        public override void ApplyChange(IDomainEvent @event, bool isRebuild)
        {
            base.ApplyChange(@event, isRebuild);
            
            if (@event is PaymentRecordCreatedEvent paymentRecordCreated)
            {
                OrderId = paymentRecordCreated.OrderId;
                PayableAmount = paymentRecordCreated.PayableAmount;
                PaidAmount = 0m;
                State = PaymentState.Unpaid;
            }
        }
    }
}