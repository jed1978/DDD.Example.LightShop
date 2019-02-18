namespace DDD.Example.LightShop.Cores.OrderContext
{
    public class Product
    {
        public long Id { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public static Product Prepare(long id, string productName, int quantity, decimal unitPrice)
        {
            return new Product(id, productName, quantity, unitPrice);
        }

        private Product(long id, string productName, int quantity, decimal unitPrice)
        {
            Id = id;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal SubTotal()
        {
            return UnitPrice * Quantity;
        }
    }
}