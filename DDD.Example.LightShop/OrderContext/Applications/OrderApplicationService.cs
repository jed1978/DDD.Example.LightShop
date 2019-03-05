using System;
using System.Collections.Generic;
using DDD.Example.LightShop.OrderContext.Domain;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.OrderContext.Applications
{
    public class OrderApplicationService
    {
        private readonly IRepository<Order> _eventStoreRepository;

        public OrderApplicationService(IRepository<Order> eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void CreateOrder(List<Product> orderItems, ShippingInfo shippingInfo)
        {
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);
            _eventStoreRepository.Save(order);
        }
    }
}