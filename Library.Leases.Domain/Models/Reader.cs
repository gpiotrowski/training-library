using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Leases.Domain.Events;
using Library.Leases.Domain.Exceptions;
using Library.Leases.Domain.Models.ValueObjects;

namespace Library.Leases.Domain.Models
{
    public class Reader : AggregateRoot
    {
        public string Name { get; set; }
        public TimeSpan MaxLeasePeriod { get; private set; }
        public int MaxConcurrentLeases { get; private set; }
        public ReaderScore Score { get; private set; }

        private List<Lease> Leases { get; set; }

        public Reader(Guid id)
        {
            Id = id;
            Leases = new List<Lease>();

            MaxLeasePeriod = TimeSpan.FromDays(21);
            MaxConcurrentLeases = 3;
            Score = ReaderScore.Zero;
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
            if (MaxConcurrentLeases > GetActiveLeases().Count)
            {
                var lease = new Lease(bookId);
                Leases.Add(lease);
                RiseEvent(new BookLeased(bookId));
            }
            else
            {
                throw new MaxConcurrentLeasesExceeded();
            }
        }

        public void IncreaseScore(int points)
        {
            Score = Score + new ReaderScore(points);
        }
    }
}
