using System;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.DomainEvents
{
    public class OrderItemAddedEvent : IDomainEvent
    {
        public OrderItemAddedEvent(Guid orderId, int productId, string itemName, decimal unitPrice)
        {
            AggregateRootId = orderId;
            ProductId = productId;
            EventId = Guid.NewGuid();
            ItemName = itemName;
            UnitPrice = unitPrice;
            OccuredOn = DateTime.Now;
        }

        public Guid EventId { get; }
        public Guid AggregateRootId { get; }
        public int ProductId { get; }
        public string ItemName { get; }
        public decimal UnitPrice { get; }
        public DateTime OccuredOn { get; }
    }
}