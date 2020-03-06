using System;
using Library.Leases.Domain.Exceptions;

namespace Library.Leases.Domain.Models.ValueObjects
{
    public readonly struct ReaderScore : IEquatable<ReaderScore>
    {
        public int Points { get; }

        public ReaderScore(int points)
        {
            if (points < 0)
            {
                throw new InvalidReaderScoreException("Reader score couldn't be negative");
            }

            Points = points;
        }

        public static ReaderScore Zero => new ReaderScore(0);

        public static ReaderScore operator +(ReaderScore scoreA, ReaderScore scoreB)
        {
            return new ReaderScore(scoreA.Points + scoreB.Points);
        }

        public static ReaderScore operator -(ReaderScore scoreA, ReaderScore scoreB)
        {
            var newPoints = scoreA.Points - scoreB.Points;

            return new ReaderScore(newPoints < 0 ? 0 : newPoints);
        }

        public bool Equals(ReaderScore other)
        {
            return Points == other.Points;
        }

        public override bool Equals(object obj)
        {
            return obj is ReaderScore other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Points;
        }
    }
}
