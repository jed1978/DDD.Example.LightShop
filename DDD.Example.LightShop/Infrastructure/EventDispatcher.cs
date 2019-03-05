using System;
using System.Collections.Generic;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.Infrastructure
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, List<IEventHandler<IDomainEvent>>> _handlers;
        private static readonly Lazy<EventDispatcher> LazyEventDispatcher = new Lazy<EventDispatcher>(() => new EventDispatcher());

        public static EventDispatcher Instance()
        {
            return LazyEventDispatcher.Value;
        }

        private EventDispatcher()
        {
            _handlers = new Dictionary<Type, List<IEventHandler<IDomainEvent>>>();
        }

        public void Register<T>(IEventHandler<IDomainEvent> handler)
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                if (!_handlers[typeof(T)].Exists(m => m.GetType() == _handlers.GetType()))
                {
                    _handlers[typeof(T)].Add(handler);
                }
            }
            else
            {
                _handlers.Add(typeof(T), new List<IEventHandler<IDomainEvent>> {handler});
            }
        }

        public void Dispatch(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                _handlers[@event.GetType()].ForEach(handler => handler.Handle(@event));
            }
        }
    }
}