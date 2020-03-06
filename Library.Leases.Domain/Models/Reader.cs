using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;

namespace Library.Leases.Domain.Models
{
    public class Reader : AggregateRoot
    {
        public string Name { get; set; }
        public int MaxLeasePeriod { get; }
        public int MaxConcurrentLeases { get; }
        public int Score { get; }

        private List<Lease> Leases { get; set; }

        public Reader(Guid id)
        {
            Id = id;
            Leases = new List<Lease>();

            MaxLeasePeriod = 21;
            MaxConcurrentLeases = 3;
            Score = 0;
        }

        public void SetReaderName(string name)
        {
            Name = name;
        }

        public List<Lease> GetActiveLeases()
        {
            return Leases.Where(x => !x.ReturnDate.HasValue).ToList();
        }

        public void LeaseBook(int bookId)
        {
            if (MaxConcurrentLeases >= GetActiveLeases().Count)
            {
                var lease = new Lease(bookId);
                Leases.Add(lease);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
