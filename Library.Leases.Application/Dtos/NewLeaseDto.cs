using System;

namespace Library.Leases.Application.Dtos
{
    public class NewLeaseDto
    {
        public Guid ReaderId { get; set; }
        public int BookId { get; set; }
    }
}