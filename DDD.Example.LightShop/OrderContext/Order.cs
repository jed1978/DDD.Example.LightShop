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
            var @event = new OrderCreatedEvent(Id, orderItems, shippingInfo);
            UncommittedEvents.Enqueue(@event);
        }
    }
}