using System;
using DDD.Example.LightShop.Cores.OrderDomain;

namespace DDD.Example.LightShop.Cores.Events
{
    public class OrderCreatedEvent : EventBase
    {
        public Product Product { get; }

        public ShippingInfo ShippingInfo { get; }

        private OrderCreatedEvent(Guid aggregateRootId, Product product, ShippingInfo shippingInfo)
        {
            AggregateRootId = aggregateRootId;
            Product = product;
            ShippingInfo = shippingInfo;
        }

        public static OrderCreatedEvent Prepare(Guid aggregateRootId, Product product, ShippingInfo shippingInfo)
        {
            return new OrderCreatedEvent(aggregateRootId, product, shippingInfo);
        }

    }
}