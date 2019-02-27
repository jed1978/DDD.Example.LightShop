using System;
using System.Collections.Generic;

namespace DDD.Example.LightShop.OrderContext
{
    public class Order
    {
        public static Order NewOrder(Guid Id)
        {
            return new Order(Id);
        }

        private Order(Guid Id)
        {
            this.Id = Id;
            UncommittedEvents = new Queue<OrderCreatedEvent>();
        }

        public Guid Id { get; private set; }

        public Queue<OrderCreatedEvent> UncommittedEvents { get; }

        public void Create(List<Product> orderItems, ShippingInfo shippingInfo)
        {
            ApplyChange(OrderCreatedEvent.NewOrderCreatedEvent(Id, orderItems, shippingInfo));
        }

        private void ApplyChange(OrderCreatedEvent @event)
        {
            UncommittedEvents.Enqueue(@event);
        }
    }
}