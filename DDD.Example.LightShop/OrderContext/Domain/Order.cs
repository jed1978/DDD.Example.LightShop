using System;
using System.Collections.Generic;
using DDD.Example.LightShop.DomainEvents;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.OrderContext.Domain
{
    public class Order : IAggregateRoot<IDomainEvent>
    {

        public Guid Id { get; private set; }
        public Queue<IDomainEvent> UncommittedEvents { get; }
        public List<Product> OrderItems { get; } = new List<Product>();
        public ShippingInfo ShippingInfo { get; private set; }


        private Order(Guid Id)
        {
            this.Id = Id;
            UncommittedEvents = new Queue<IDomainEvent>();
        }
        
        public static Order NewOrder(Guid Id)
        {
            return new Order(Id);
        }

        public void Create(List<Product> orderItems, ShippingInfo shippingInfo)
        {
            var subtotal = 0m;
            foreach (var item in orderItems)
            {
                ApplyChange(new OrderItemAddedEvent(Id, item.Id, item.ItemName, item.UnitPrice));
                subtotal += item.UnitPrice;
            }
            ApplyChange(new ShippingInfoUpdatedEvent(Id, shippingInfo.ContactName, shippingInfo.ContactPhone, shippingInfo.ShippingAddress));
            
            ApplyChange(OrderCreatedEvent.NewOrderCreatedEvent(Id, subtotal));
        }

        public void Commit()
        {
            UncommittedEvents.Clear();
            
        }

        public void Rebuild(IEnumerable<IDomainEvent> historicalEvents)
        {
            foreach (var @event in historicalEvents)
            {
                ApplyChange(@event, true);
            }
        }

        public void ApplyChange(IDomainEvent @event, bool isRebuild = false)
        {
            if (!isRebuild)
            {
                UncommittedEvents.Enqueue(@event);
            }

            if (@event is OrderItemAddedEvent orderItemAdded)
            {
                OrderItems.Add(Product.NewProduct(orderItemAdded.ProductId, orderItemAdded.ItemName, orderItemAdded.UnitPrice));
            }

            if (@event is ShippingInfoUpdatedEvent shippingInfoUpdated)
            {
                ShippingInfo = ShippingInfo.NewShippingInfo(shippingInfoUpdated.ContactName, shippingInfoUpdated.ContactPhone, shippingInfoUpdated.ShippingAddress);
            }
        }
    }
}