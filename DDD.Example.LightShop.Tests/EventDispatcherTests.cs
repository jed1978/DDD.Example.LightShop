using System;
using System.Collections.Generic;
using DDD.Example.LightShop.Infrastructure;
using DDD.Example.LightShop.SharedKernel;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace DDD.Example.LightShop.Tests
{
    [TestFixture]
    public class EventDispatcherTests
    {
        [Test]
        public void Test_Should_Publish_One_Events_to_Registered_EventHandlers()
        {
            var dispatcher = new EventDispatcher();

            var handler = Substitute.For<IEventHandler<IDomainEvent>>();

            dispatcher.Register<TestEvent>(handler);
            var testEvent = new TestEvent();
            var events = new List<TestEvent> {testEvent};
            dispatcher.Dispatch(events);

            handler.Received(1).Handle(testEvent);
        }

        [Test]
        public void Test_Should_Publish_Three_Events_to_Registered_EventHandlers()
        {
            var dispatcher = new EventDispatcher();

            var handler = Substitute.For<IEventHandler<IDomainEvent>>();

            dispatcher.Register<TestEvent>(handler);
            
            var events = new List<TestEvent>();
   
            for (int i = 0; i < 3; i++)
            {
                var testEvent = new TestEvent();
                events.Add(testEvent);
            }
            dispatcher.Dispatch(events);

            handler.Received(3).Handle(Arg.Any<TestEvent>());
        }
        
        [Test]
        public void Test_FactoryMethod_Return_Singleton()
        {
            var dispatcher1 = EventDispatcher.Instance();

            var dispatcher2 = EventDispatcher.Instance();

            dispatcher1.Should().Be(dispatcher2);

        }
        
        public class TestEvent : IDomainEvent
        {
            public Guid EventId { get; }
            public Guid AggregateRootId { get; }
            public DateTime OccuredOn { get; }
        }
    }
}