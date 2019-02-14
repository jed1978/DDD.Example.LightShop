using System;

namespace DDD.Example.LightShop.Cores
{
    public abstract class EventBase
    {
        protected EventBase()
        {
            Id = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
        }

        protected Guid AggregateRootId { get; set; }
        public long Id { get; set; }
    }
}