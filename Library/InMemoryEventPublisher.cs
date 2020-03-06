using System;
using System.Collections.Concurrent;
using System.Linq;
using Library.Core;
using Library.Core.Infrastructure;

namespace Library
{
    public class InMemoryEventPublisher : IEventPublisher
    {
        private readonly BlockingCollection<ISubscriber> _subscribers;

        public InMemoryEventPublisher()
        {
            _subscribers = new BlockingCollection<ISubscriber>();
        }

        public void Publish(IDomainEvent @event)
        {
            var handlerType = @event.GetType().AssemblyQualifiedName;

            var handlers = _subscribers.Where(x => x.GetHandlerType().Equals(handlerType));

            foreach (var handler in handlers)
            {
                handler.Publish(@event);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IDomainEvent
        {
            var subscriber = new Subscriber<TEvent>()
            {
                HandlerType = typeof(TEvent).AssemblyQualifiedName,
                Handler = handler
            };

            _subscribers.Add(subscriber);
        }

        private class Subscriber<TEvent> : ISubscriber where TEvent : IDomainEvent
        {
            public string HandlerType { get; set; }
            public Action<TEvent> Handler { get; set; }

            public void Publish(IDomainEvent message)
            {
                Handler.Invoke((TEvent)message);
            }

            public string GetHandlerType()
            {
                return HandlerType;
            }
        }

        private interface ISubscriber
        {
            void Publish(IDomainEvent @event);
            string GetHandlerType();
        }
    }
}
