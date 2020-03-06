using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Infrastructure
{
    public interface IEventPublisher
    {
        void Publish(IDomainEvent @event);

        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IDomainEvent;
    }
}
