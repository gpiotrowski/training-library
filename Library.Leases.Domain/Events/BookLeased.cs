using System;
using Library.Core;

namespace Library.Leases.Domain.Events
{
    public class BookLeased : IDomainEvent
    {
        public Guid EventId { get; }
        public int BookId { get; }

        public BookLeased(int bookId)
        {
            EventId = Guid.NewGuid();
            BookId = bookId;
        }

    }
}
