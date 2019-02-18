using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Example.LightShop.Cores.Events;
using DDD.Example.LightShop.Cores.OrderContext;

namespace DDD.Example.LightShop.Applications
{
    public class OrderRepository
    {
        private readonly Dictionary<Guid, Dictionary<long, OrderCreatedEvent>> _repository = new Dictionary<Guid, Dictionary<long, OrderCreatedEvent>>();

        public void Save(Order order)
        {
            try
            {
                _repository.TryAdd(order.Id, new Dictionary<long, OrderCreatedEvent>());
                foreach (var @event in order.UncommittedChanges())
                {
                    _repository[order.Id][@event.Id] = @event;
                }

                order.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Order Load(Guid id)
        {
            var historicalEvents = _repository[id];
            var order = Order.Prepare(id);
            order.Rebuild(historicalEvents.Values.ToList());
            return order;
        }
    }
}