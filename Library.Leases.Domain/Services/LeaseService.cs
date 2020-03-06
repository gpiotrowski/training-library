using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Leases.Domain.Dtos;
using Library.Leases.Domain.Exceptions;
using Library.Leases.Domain.Models;
using Library.Leases.Domain.Stores;

namespace Library.Leases.Domain.Services
{
    public class LeaseService
    {
        private readonly IReaderStore _readerStore;
        private readonly IBookCatalogueStore _bookCatalogueStore;

        public LeaseService(IBookCatalogueStore bookCatalogueStore, IReaderStore readerStore)
        {
            _readerStore = readerStore;
            _bookCatalogueStore = bookCatalogueStore;
        }

        public OperationStatus LeaseBook(NewLeaseDto leaseDto)
        {
            try
            {
                var requestedBook = _bookCatalogueStore.GetBookById(leaseDto.BookId);
                if (requestedBook?.AvailableQuantity > 0)
                {
                    var reader = _readerStore.GetReaderById(leaseDto.ReaderId);
                    reader.LeaseBook(leaseDto.BookId);

                    requestedBook.AvailableQuantity--;

                    _readerStore.SaveReader(reader);
                    _bookCatalogueStore.UpdateBookQuantity(requestedBook.Id, requestedBook.AvailableQuantity);

                    return OperationStatus.CompletedSuccessfully;
                }

                return new OperationStatus()
                {
                    Success = false,
                    ErrorMessage = "Requested book is not available"
                };
            }
            catch (MaxConcurrentLeasesExceeded e)
            {
                return new OperationStatus()
                {
                    Success = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public IEnumerable<LeaseDto> GetReaderOrders(Guid readerId)
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
