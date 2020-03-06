using System;

namespace Library.Leases.Domain.Models
{
    public class Lease
    {
        public int BookId { get; protected set; }
        public DateTime OrderDate { get; protected set; }
        public DateTime? ReturnDate { get; protected set; }

        public Lease(int bookId)
        {
            BookId = bookId;
            OrderDate = DateTime.UtcNow;
        }
    }
}