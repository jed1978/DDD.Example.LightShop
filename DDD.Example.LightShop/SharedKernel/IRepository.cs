namespace DDD.Example.LightShop.SharedKernel
{
    public interface IRepository
    {
        void Save(IAggregateRoot<IDomainEvent> aggregateRoot);
    }
}