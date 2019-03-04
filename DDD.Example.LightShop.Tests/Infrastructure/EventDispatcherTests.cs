using System;
using System.Collections.Generic;
using DDD.Example.LightShop.Infrastructure;
using DDD.Example.LightShop.SharedKernel;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests.Infrastructure
{
    [TestFixture]
    public class EventDispatcherTests
    {
        [Test]
        public void Test_Should_Publish_One_Events_to_Registered_EventHandlers()
        {
            var dispatcher = GetEventDispatcher(out var handler);

            WhenDispatch(dispatcher, 1);

            handler.Received(1).Handle(Arg.Any<TestEvent>());
        }


        [Test]
        public void Test_Should_Publish_Three_Events_to_Registered_EventHandlers()
        {
            var dispatcher = GetEventDispatcher(out var handler);

            WhenDispatch(dispatcher, 3);

            handler.Received(3).Handle(Arg.Any<TestEvent>());
        }
        
        [Test]
        public void Test_FactoryMethod_Return_Singleton()
        {
            var dispatcher1 = EventDispatcher.Instance();

            var dispatcher2 = EventDispatcher.Instance();

            dispatcher1.Should().Be(dispatcher2);

        }

        private static EventDispatcher GetEventDispatcher(out IEventHandler<IDomainEvent> handler)
        {
            var dispatcher = EventDispatcher.Instance();
            handler = Substitute.For<IEventHandler<IDomainEvent>>();
            dispatcher.Register<TestEvent>(handler);
            return dispatcher;
        }
        
        private static void WhenDispatch(EventDispatcher dispatcher, int eventCount)
        {
            var events = new List<TestEvent>();
            for (int i = 0; i < eventCount; i++)
            {
                var testEvent = new TestEvent();
                events.Add(testEvent);
            }

            dispatcher.Dispatch(events);
        }

        private class TestEvent : IDomainEvent
        {
            public Guid EventId { get; }
            public Guid AggregateRootId { get; }
            public DateTime OccuredOn { get; }
        }
    }
}