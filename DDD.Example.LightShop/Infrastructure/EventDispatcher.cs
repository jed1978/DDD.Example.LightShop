using System;
using System.Collections.Generic;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.Infrastructure
{
    public class EventDispatcher : IEventDispatcher
    {
        private static readonly Dictionary<Type, List<IEventHandler<IDomainEvent>>> Handlers;
        private static readonly Lazy<EventDispatcher> LazyEventDispatcher = new Lazy<EventDispatcher>(() => new EventDispatcher());

        public static EventDispatcher Instance()
        {
            return LazyEventDispatcher.Value;
        }

        static EventDispatcher()
        {
            Handlers = new Dictionary<Type, List<IEventHandler<IDomainEvent>>>();
        }

        public virtual void Register<T>(IEventHandler<IDomainEvent> handler)
        {
            if (Handlers.ContainsKey(typeof(T)))
            {
                if (!Handlers[typeof(T)].Exists(m => m.GetType() == Handlers.GetType()))
                {
                    Handlers[typeof(T)].Add(handler);
                }
            }
            else
            {
                Handlers.Add(typeof(T), new List<IEventHandler<IDomainEvent>> {handler});
            }
        }

        public virtual void Dispatch(IEnumerable<IDomainEvent> events)
        {

            foreach (var @event in events)
            {
                Handlers[@event.GetType()].ForEach(handler => handler.Handle(@event));
            }
        }


    }
}