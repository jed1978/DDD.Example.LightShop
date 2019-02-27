namespace DDD.Example.LightShop.OrderContext.Domain
{
    public class ShippingInfo
    {
        public static ShippingInfo NewShippingInfo(string contactName, string contactPhone, string shippingAddress)
        {
            return new ShippingInfo(contactName, contactPhone, shippingAddress);
        }

        private ShippingInfo(string contactName, string contactPhone, string shippingAddress)
        {
            ContactName = contactName;
            ContactPhone = contactPhone;
            ShippingAddress = shippingAddress;
        }

        public string ContactName { get; private set; }
        public string ContactPhone { get; private set; }
        public string ShippingAddress { get; private set; }
    }
}