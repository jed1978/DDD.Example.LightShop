using System.Collections.Generic;
using DDD.Example.LightShop.OrderContext.DomainEvents;
using DDD.Example.LightShop.SharedKernel;

namespace DDD.Example.LightShop.Infrastructure
{
    public interface IEventDispatcher
    {
        void Dispatch(IEnumerable<IDomainEvent> events);
    }
}