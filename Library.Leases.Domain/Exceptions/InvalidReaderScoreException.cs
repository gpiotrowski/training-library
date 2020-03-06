using Library.Core;

namespace Library.Leases.Domain.Exceptions
{
    public class InvalidReaderScoreException : DomainException
    {
        public InvalidReaderScoreException() : base("Invalid reader score")
        {
        }

        public InvalidReaderScoreException(string message) : base(message)
        {
            
        }
    }
}
