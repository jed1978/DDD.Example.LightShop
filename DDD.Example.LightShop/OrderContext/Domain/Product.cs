namespace DDD.Example.LightShop.OrderContext.Domain
{
    public class Product
    {
        private Product(int id, string itemName, decimal unitPrice)
        {
            Id = id;
            ItemName = itemName;
            UnitPrice = unitPrice;
        }

        public static Product NewProduct(int id, string itemName, decimal unitPrice)
        {
            return new Product(id, itemName, unitPrice);
        }

        public int Id { get; private set; }
        public string ItemName { get; private set; }
        public decimal UnitPrice { get; private set; }
    }
}