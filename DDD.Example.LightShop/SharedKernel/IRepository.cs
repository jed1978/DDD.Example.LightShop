using System;
using DDD.Example.LightShop.OrderContext.Domain;

namespace DDD.Example.LightShop.SharedKernel
{
    public interface IRepository<T> where T: EntityBase
    {
        void Save(T entity);
        T Load(Guid id);
    }
}