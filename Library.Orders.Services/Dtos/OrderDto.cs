using System;

namespace Library.Orders.Services.Dtos
{
    public class OrderDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public DateTime Date { get; set; }
    }
}