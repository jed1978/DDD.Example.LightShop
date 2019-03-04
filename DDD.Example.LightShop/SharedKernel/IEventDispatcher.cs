using System.Collections.Generic;

namespace DDD.Example.LightShop.SharedKernel
{
    public interface IEventDispatcher
    {
        void Dispatch(IEnumerable<IDomainEvent> events);
    }
}