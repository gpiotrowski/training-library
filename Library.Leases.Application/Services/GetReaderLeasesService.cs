using System;
using System.Collections.Generic;
using System.Linq;
using Library.Items.Services.Services;
using Library.Leases.Application.Dtos;
using Library.Leases.Domain.Stores;

namespace Library.Leases.Application.Services
{
    public class GetReaderLeasesService
    {
        private readonly ItemService _itemService;
        private readonly IReaderStore _readerStore;

        public GetReaderLeasesService(IReaderStore readerStore, ItemService itemService)
        {
            _itemService = itemService;
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

            var itemsFromOrders = _itemService.GetItemsByIds(orderDtos.Select(x => x.BookId));

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
