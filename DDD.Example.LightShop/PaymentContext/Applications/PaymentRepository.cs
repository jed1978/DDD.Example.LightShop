using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Example.LightShop.PaymentContext.Domain;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.PaymentContext.Applications
{
    public class PaymentRepository : IRepository<Payment>
    {
        private readonly IEventDispatcher _eventDispatcher;

        private readonly Dictionary<Guid, Queue<IDomainEvent>>
            _eventStore = new Dictionary<Guid, Queue<IDomainEvent>>();

        public PaymentRepository(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public void Save(Payment entity)
        {
            if (!_eventStore.ContainsKey(entity.Id))
                _eventStore.Add(entity.Id, new Queue<IDomainEvent>());

            var list = new List<IDomainEvent>();

            while (entity.UncommittedEvents.Count > 0)
            {
                var uncommittedEvent = entity.UncommittedEvents.Dequeue();
                _eventStore[entity.Id].Enqueue(uncommittedEvent);
                list.Add(uncommittedEvent);
            }

            _eventDispatcher.Dispatch(list);
            entity.Commit();
        }

        public Payment Load(Guid id)
        {
            Payment entity = null;
            if (!_eventStore.ContainsKey(id)) return entity;
            entity = Payment.NewPayment(id);
            var historicalEvents = _eventStore[id].ToList();
            entity.Rebuild(historicalEvents);

            return entity;
        }
    }
}