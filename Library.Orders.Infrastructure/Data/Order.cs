using System;

namespace Library.Infrastructure.Data
{
    public class Order
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}