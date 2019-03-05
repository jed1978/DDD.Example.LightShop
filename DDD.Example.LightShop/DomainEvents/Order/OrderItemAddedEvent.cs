using System;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.DomainEvents.Order
{
    public class OrderItemAddedEvent : IDomainEvent
    {
        public static OrderItemAddedEvent Raise(Guid orderId, int productId, string itemName, decimal unitPrice)
        {
            return new OrderItemAddedEvent(orderId, productId, itemName, unitPrice);
        }

        private OrderItemAddedEvent(Guid orderId, int productId, string itemName, decimal unitPrice)
        {
            AggregateRootId = orderId;
            ProductId = productId;
            EventId = Guid.NewGuid();
            ItemName = itemName;
            UnitPrice = unitPrice;
            OccuredOn = DateTime.Now;
        }

        public Guid EventId { get; private set;}
        public Guid AggregateRootId { get; private set;}
        public int ProductId { get; private set;}
        public string ItemName { get; private set;}
        public decimal UnitPrice { get; private set;}
        public DateTime OccuredOn { get; private set;}
    }
}