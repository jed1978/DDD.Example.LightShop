using System;
using DDD.Example.LightShop.Cores.OrderContext;

namespace DDD.Example.LightShop.Cores.Events
{
    public class CreateOrderCommand
    {
        public Product Product { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
        public Guid AggregateRootId { get; set; }

        public static CreateOrderCommand Prepare(Guid aggregateRootId, Product product, ShippingInfo shippingInfo)
        {
            return new CreateOrderCommand
            {
                AggregateRootId = aggregateRootId,
                Product = product,
                ShippingInfo = shippingInfo
            };
        }
    }
}