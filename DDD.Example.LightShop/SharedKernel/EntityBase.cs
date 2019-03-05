using System;
using System.Collections.Generic;

namespace DDD.Example.LightShop.SharedKernel
{
    public abstract class EntityBase
    {
        public Queue<IDomainEvent> UncommittedEvents { get; protected set; }
        public Guid Id { get; protected set; }

        protected EntityBase(Guid id)
        {
            Id = id;
            UncommittedEvents = new Queue<IDomainEvent>();
        }
        
        public virtual void Commit()
        {
            UncommittedEvents.Clear();
        }

        public virtual void ApplyChange(IDomainEvent @event, bool isRebuild)
        {
            if (!isRebuild)
            {
                UncommittedEvents.Enqueue(@event);
            }
        }

        public void Rebuild(IEnumerable<IDomainEvent> historicalEvents)
        {
            foreach (var @event in historicalEvents)
            {
                ApplyChange(@event, true);
            }
        }
    }
}