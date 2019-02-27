using System;

namespace DDD.Example.LightShop.SharedKernel
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        Guid AggregateRootId { get; }
        DateTime OccuredOn { get; }
    }
}