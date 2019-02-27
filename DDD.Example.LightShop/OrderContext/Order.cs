using System;

namespace DDD.Example.LightShop.OrderContext
{
    public class Order
    {
        public Order(Guid Id)
        {
            this.Id = Id;
        }

        public Guid Id { get; private set; }
    }
}