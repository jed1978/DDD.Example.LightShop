namespace DDD.Example.LightShop.SharedKernel
{
    public interface IEventHandler<T> where T: IDomainEvent
    {
        void Handle(T domainEvent);
    }
}