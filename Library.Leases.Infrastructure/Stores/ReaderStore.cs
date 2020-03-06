using System;
using System.Collections.Generic;
using System.Linq;
using Library.Leases.Domain.Models;
using Library.Leases.Domain.Stores;

namespace Library.Leases.Infrastructure.Stores
{
    public class ReaderStore : IReaderStore
    {
        private List<Reader> _readers { get; set; }

        public ReaderStore()
        {
            var readerA = new Reader(Guid.Parse("ef372759-14f4-410d-a59d-9b5174ba50ba"));
            readerA.SetReaderName("Jan Kowalski");

            var readerB = new Reader(Guid.Parse("e58e0ba5-020e-4454-9a57-2d0c8fc2b711"));
            readerB.SetReaderName("Katarzyna Nowak");

            _readers = new List<Reader>()
            {
                readerA,
                readerB
            };
        }

        public Reader GetReaderById(Guid id)
        {
            return _readers.SingleOrDefault(x => x.Id == id);
        }

        public void SaveReader(Reader user)
        {
            _readers.RemoveAll(x => x.Id == user.Id);
            _readers.Add(user);
        }
    }
}