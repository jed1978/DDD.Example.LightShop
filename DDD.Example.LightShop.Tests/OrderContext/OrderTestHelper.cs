using System.Collections.Generic;
using DDD.Example.LightShop.OrderContext.Domain;

namespace DDD.Example.LightShop.Tests.OrderContext
{
    public class OrderTestHelper
    {
        public static (List<Product> orderItems, ShippingInfo shippingInfo) Given_OrderDetailsWasPrepared()
        {
            var orderItems = new List<Product>
            {
                Product.NewProduct(10001, "Apple Mac Book Pro 13 inch no touch bar", 43900m)
            };
            var shippingInfo = ShippingInfo.NewShippingInfo("王小明", "0988123567", "忠孝東路一段100號");
            return (orderItems, shippingInfo);
        }
    }
}