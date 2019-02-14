using System;
using System.Collections.Generic;
using DDD.Example.LightShop.Cores.Events;

namespace DDD.Example.LightShop.Cores.OrderDomain
{
    public class Order : AggregateRoot<OrderCreatedEvent>
    {
        private readonly List<OrderCreatedEvent> _changes = new List<OrderCreatedEvent>();
        public Product Product { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
        public long OrderId { get; set; }
        
        public static Order Prepare(Guid aggregateRootId)
        {
            return new Order(aggregateRootId);
        }

        private Order(Guid aggregateRootId)
        {
            Id = aggregateRootId;
        }

        public void Create(CreateOrderCommand createOrderCommand)
        {
            ApplyChange(OrderCreatedEvent.Prepare(createOrderCommand.AggregateRootId, createOrderCommand.Product,
                createOrderCommand.ShippingInfo));
        }

        protected override void ApplyChange(OrderCreatedEvent orderCreatedEvent, bool isRebuild = false)
        {
            Product = orderCreatedEvent.Product;
            ShippingInfo = orderCreatedEvent.ShippingInfo;
            if (!isRebuild)
            {
                _changes.Add(orderCreatedEvent);
            }
        }

        public override List<OrderCreatedEvent> UncommittedChanges()
        {
            return _changes;
        }

        public override void Rebuild(List<OrderCreatedEvent> historicalEvents)
        {
            foreach (var @event in historicalEvents)
            {
                ApplyChange(@event, true);
            }
        }

        public override void Commit()
        {
            _changes.Clear();
        }
    }
}