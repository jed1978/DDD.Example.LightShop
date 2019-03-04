using System;
using System.Collections.Generic;

namespace DDD.Example.LightShop.SharedKernel
{
    public interface IAggregateRoot<TDomainEvent>
    {
        Guid Id { get; }
        Queue<TDomainEvent> UncommittedEvents { get; }
        void ApplyChange(TDomainEvent @event, bool isRebuild);
        void Commit();
    }
}