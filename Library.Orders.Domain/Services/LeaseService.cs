using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Leases.Domain.Dtos;
using Library.Leases.Domain.Entities;
using Library.Leases.Domain.Stores;

namespace Library.Leases.Domain.Services
{
    public class LeaseService
    {
        private readonly IUserStore _userStore;
        private readonly ILeaseStore _leaseStore;
        private readonly IBookCatalogueStore _bookCatalogueStore;

        public LeaseService(IBookCatalogueStore bookCatalogueStore, IUserStore userStore, ILeaseStore leaseStore)
        {
            _userStore = userStore;
            _leaseStore = leaseStore;
            _bookCatalogueStore = bookCatalogueStore;
        }

        public OperationStatus PlaceOrder(NewLeaseDto leaseDto)
        {
            var requestedBook = _bookCatalogueStore.GetBookById(leaseDto.BookId);
            if (requestedBook?.AvailableQuantity > 0)
            {
                var user = _userStore.GetUserById(leaseDto.UserId);
                var inProgressOrders = _leaseStore.GetUserLeaseInProgressQty(leaseDto.UserId);

                if (user.BookLimit >= inProgressOrders)
                {
                    requestedBook.AvailableQuantity--;

                    _userStore.SaveUser(user);

                    var order = new Lease()
                    {
                        BookId = leaseDto.BookId,
                        IsReturned = false,
                        OrderDate = DateTime.UtcNow,
                        ReturnDate = null,
                        UserId = leaseDto.UserId
                    };

                    _bookCatalogueStore.UpdateBookQuantity(requestedBook.Id, requestedBook.AvailableQuantity);
                    _leaseStore.Lease(order);

                    return OperationStatus.CompletedSuccessfully;
                }
                else
                {
                    return new OperationStatus()
                    {
                        Success = false,
                        ErrorMessage = "Book limit exceeded"
                    };
                }
            }

            return new OperationStatus()
            {
                Success = false,
                ErrorMessage = "Requested book is not available"
            };
        }

        public IEnumerable<LeaseDto> GetUserOrders(int userId)
        {
            var userOrders = _leaseStore.GetUserLeases(userId);

            var orderDtos = userOrders.Select(x => new LeaseDto()
            {
                UserId = x.UserId,
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
