using System;

namespace Library.Leases.Domain.Dtos
{
    public class LeaseDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public DateTime Date { get; set; }
    }
}