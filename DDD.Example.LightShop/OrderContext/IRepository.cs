namespace DDD.Example.LightShop.OrderContext
{
    public interface IRepository
    {
        void Save(IAggregateRoot aggregateRoot);
    }

    public interface IAggregateRoot
    {
    }
}