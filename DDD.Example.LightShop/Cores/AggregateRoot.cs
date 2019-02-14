using System;
using System.Collections.Generic;
using DDD.Example.LightShop.Cores.Events;

namespace DDD.Example.LightShop.Cores
{
    public abstract class AggregateRoot<TEvent> : Entity where TEvent : EventBase 
    {
        public Guid Id { get; set; }

        protected abstract void ApplyChange(TEvent @event, bool isRebuild = false);
        public abstract List<TEvent> UncommittedChanges();
        public abstract void Rebuild(List<TEvent> historicalEvents);
        public abstract void Commit();
    }
}