using System;
using System.Collections.Generic;
using DDD.Example.LightShop.DomainEvents;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.OrderContext.Domain
{
    public class Order : EntityBase, IAggregateRoot<IDomainEvent>
    {
        public List<Product> OrderItems { get; } = new List<Product>();
        public ShippingInfo ShippingInfo { get; private set; }
        public decimal Subtotal { get; private set; }


        private Order(Guid Id) : base(Id)
        {
        }

        public static Order NewOrder(Guid Id)
        {
            return new Order(Id);
        }

        public void Create(IEnumerable<Product> orderItems, ShippingInfo shippingInfo)
        {
            var subtotal = 0m;
            foreach (var item in orderItems)
            {
                ApplyChange(new OrderItemAddedEvent(Id, item.Id, item.ItemName, item.UnitPrice));
                subtotal += item.UnitPrice;
            }

            ApplyChange(new ShippingInfoUpdatedEvent(Id, shippingInfo.ContactName, shippingInfo.ContactPhone,
                shippingInfo.ShippingAddress));

            ApplyChange(OrderCreatedEvent.NewOrderCreatedEvent(Id, subtotal));
            
        }

        public override void ApplyChange(IDomainEvent @event, bool isRebuild = false)
        {
            base.ApplyChange(@event, isRebuild);
            
            switch (@event)
            {
                case OrderItemAddedEvent orderItemAdded:
                    OrderItems.Add(Product.NewProduct(orderItemAdded.ProductId, orderItemAdded.ItemName,
                        orderItemAdded.UnitPrice));
                    break;
                case ShippingInfoUpdatedEvent shippingInfoUpdated:
                    ShippingInfo = ShippingInfo.NewShippingInfo(shippingInfoUpdated.ContactName,
                        shippingInfoUpdated.ContactPhone, shippingInfoUpdated.ShippingAddress);
                    break;
                case OrderCreatedEvent orderCreatedEvent:
                    Subtotal = orderCreatedEvent.Subtotal;
                    break;
            }
        }
    }
}