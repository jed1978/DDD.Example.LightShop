using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Example.LightShop.Infrastructure;
using DDD.Example.LightShop.OrderContext.Domain;
using DDD.Example.LightShop.OrderContext.DomainEvents;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.OrderContext.Application
{
    public class OrderRepository : IRepository
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly Dictionary<Guid, Queue<OrderCreatedEvent>> _eventStore = new Dictionary<Guid, Queue<OrderCreatedEvent>>();

        public OrderRepository(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public void Save(IAggregateRoot<IDomainEvent> aggregateRoot)
        {
            if (!_eventStore.ContainsKey(aggregateRoot.Id))
            {
                _eventStore.Add(aggregateRoot.Id, new Queue<OrderCreatedEvent>());
            }
            
            var list = new List<OrderCreatedEvent>();
            
            while (aggregateRoot.UncommittedEvents.Count>0)
            {
                var uncommittedEvent = aggregateRoot.UncommittedEvents.Dequeue() as OrderCreatedEvent;
                _eventStore[aggregateRoot.Id].Enqueue(uncommittedEvent);
                list.Add(uncommittedEvent);
            }
    
            _eventDispatcher.Dispatch(list);
            aggregateRoot.Commit();
        }

        public Order Load(Guid id)
        {
            Order order = null;
            if (!_eventStore.ContainsKey(id)) return order;
            order = Order.NewOrder(id);
            var historicalEvents = _eventStore[id].ToList();
            order.Rebuild(historicalEvents);

            return order;
        }
    }
}