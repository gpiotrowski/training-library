using System;

namespace Library.Core
{
    public interface IDomainEvent
    {
        Guid EventId { get;  }
    }
}
