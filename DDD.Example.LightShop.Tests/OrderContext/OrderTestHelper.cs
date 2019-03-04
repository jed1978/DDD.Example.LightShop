using System;
using System.Collections.Generic;
using DDD.Example.LightShop.OrderContext.Domain;

namespace DDD.Example.LightShop.Tests.OrderContext
{
    public static class OrderTestHelper
    {
        public static (List<Product> orderItems, ShippingInfo shippingInfo) Given_OrderDetailsIsReady()
        {
            var orderItems = new List<Product>
            {
                Product.NewProduct(10001, "Apple Mac Book Pro 13 inch no touch bar", 43900m)
            };
            var shippingInfo = ShippingInfo.NewShippingInfo("王小明", "0988123567", "忠孝東路一段100號");
            return (orderItems, shippingInfo);
        }

        public static Order Given_OrderIsReady(List<Product> orderItems, ShippingInfo shippingInfo)
        {
            var order = Order.NewOrder(Guid.NewGuid());
            order.Create(orderItems, shippingInfo);
            return order;
        }
    }
}