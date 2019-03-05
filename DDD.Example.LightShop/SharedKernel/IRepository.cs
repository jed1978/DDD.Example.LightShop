namespace DDD.Example.LightShop.SharedKernel
{
    public interface IRepository<T> where T: EntityBase
    {
        void Save(T entity);
    }
}