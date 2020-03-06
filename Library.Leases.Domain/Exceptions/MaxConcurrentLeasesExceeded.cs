using Library.Core;

namespace Library.Leases.Domain.Exceptions
{
    public class MaxConcurrentLeasesExceeded : DomainException
    {
        public MaxConcurrentLeasesExceeded() : base("Max number of concurrent leases exceeded!")
        {
            
        }
    }
}
