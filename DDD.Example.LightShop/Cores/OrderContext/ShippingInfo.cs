namespace DDD.Example.LightShop.Cores.OrderContext
{
    public class ShippingInfo
    {
        public string ContactName { get; private set; }
        public string ContactPhoneNumber { get; private set; }
        public string ShippingAddress { get; private set; }

        public static ShippingInfo Prepare(string contactName, string contactPhoneNumber, string shippingAddress)
        {
            return new ShippingInfo(contactName, contactPhoneNumber, shippingAddress);
        }

        private ShippingInfo(string contactName, string contactPhoneNumber, string shippingAddress)
        {
            ContactName = contactName;
            ContactPhoneNumber = contactPhoneNumber;
            ShippingAddress = shippingAddress;
        }
    }
}