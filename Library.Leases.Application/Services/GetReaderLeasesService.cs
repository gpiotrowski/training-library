using System;
using System.Collections.Generic;
using System.Linq;
using Library.Leases.Application.Dtos;
using Library.Leases.Domain.Stores;

namespace Library.Leases.Application.Services
{
    public class GetReaderLeasesService
    {
        private readonly IBookCatalogueStore _bookCatalogueStore;
        private readonly IReaderStore _readerStore;

        public GetReaderLeasesService(IReaderStore readerStore, IBookCatalogueStore bookCatalogueStore)
        {
            _bookCatalogueStore = bookCatalogueStore;
            _readerStore = readerStore;
        }

        public IEnumerable<LeaseDto> GetReaderLeases(Guid readerId)
        {
            var reader = _readerStore.GetReaderById(readerId);
            var readerLeases = reader.GetActiveLeases();

            var orderDtos = readerLeases.Select(x => new LeaseDto()
            {
                ReaderId = reader.Id,
                BookId = x.BookId,
                Date = x.OrderDate
            }).ToList();

            var itemsFromOrders = _bookCatalogueStore.GetBooksByIds(orderDtos.Select(x => x.BookId));

            return orderDtos.Select(x =>
            {
                var item = itemsFromOrders.Single(i => i.Id == x.BookId);
                x.BookName = item.Name;
                x.BookAuthor = item.Author;
                return x;
            });
        }
    }
}
