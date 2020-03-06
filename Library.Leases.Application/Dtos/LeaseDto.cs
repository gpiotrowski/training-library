using System;

namespace Library.Leases.Application.Dtos
{
    public class LeaseDto
    {
        public Guid ReaderId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public DateTime Date { get; set; }
    }
}