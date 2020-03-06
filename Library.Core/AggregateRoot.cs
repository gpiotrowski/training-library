using System;
using System.Collections.Generic;

namespace Library.Core
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; set; }

        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

        public IEnumerable<IDomainEvent> FlushEvents()
        {
            var events = _events.ToArray();
            _events.Clear();

            return events;
        }

        public void RiseEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }
    }
}
