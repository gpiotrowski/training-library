using System.Collections.Generic;
using Library.Leases.Domain.Models;

namespace Library.Leases.Domain.Stores
{
    public interface IBookCatalogueStore
    {
        Book GetBookById(int id);
        IEnumerable<Book> GetBooksByIds(IEnumerable<int> ids);
        void UpdateBookQuantity(int id, int availableQuantity);
    }
}
