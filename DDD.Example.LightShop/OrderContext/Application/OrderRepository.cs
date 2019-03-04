using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Example.LightShop.Infrastructure;
using DDD.Example.LightShop.OrderContext.Domain;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.OrderContext.Application
{
    public class OrderRepository : IRepository
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly Dictionary<Guid, Queue<IDomainEvent>> _eventStore = new Dictionary<Guid, Queue<IDomainEvent>>();

        public OrderRepository(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public void Save(IAggregateRoot<IDomainEvent> aggregateRoot)
        {
            if (!_eventStore.ContainsKey(aggregateRoot.Id))
            {
                _eventStore.Add(aggregateRoot.Id, new Queue<IDomainEvent>());
            }
            
            var list = new List<IDomainEvent>();
            
            while (aggregateRoot.UncommittedEvents.Count>0)
            {
                var uncommittedEvent = aggregateRoot.UncommittedEvents.Dequeue();
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