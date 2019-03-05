using System;
using System.Collections.Generic;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.PaymentContext.Domain
{
    public class Payment : IAggregateRoot<IDomainEvent>
    {
        public Payment(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public Queue<IDomainEvent> UncommittedEvents { get; }

        public void ApplyChange(IDomainEvent @event, bool isRebuild)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}