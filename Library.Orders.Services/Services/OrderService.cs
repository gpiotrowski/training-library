using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using Library.Infrastructure.Data;
using Library.Infrastructure.Stores;
using Library.Orders.Services.Dtos;

namespace Library.Orders.Services.Services
{
    public class OrderService
    {
        private readonly UserStore _userStore;
        private readonly OrderStore _orderStore;
        private readonly ItemStore _itemStore;

        public OrderService(ItemStore itemStore, UserStore userStore, OrderStore orderStore)
        {
            _userStore = userStore;
            _orderStore = orderStore;
            _itemStore = itemStore;
        }

        public OperationStatus PlaceOrder(NewOrderDto orderDto)
        {
            var requestedBook = _itemStore.GetItemById(orderDto.BookId);
            if (requestedBook?.AvailableQuantity > 0)
            {
                var user = _userStore.GetUserById(orderDto.UserId);
                var inProgressOrders = _orderStore.GetUserOrderInProgressQty(orderDto.UserId);

                if (user.BookLimit >= inProgressOrders)
                {
                    requestedBook.AvailableQuantity--;

                    _userStore.SaveUser(user);

                    var order = new Order()
                    {
                        BookId = orderDto.BookId,
                        IsReturned = false,
                        OrderDate = DateTime.UtcNow,
                        ReturnDate = null,
                        UserId = orderDto.UserId
                    };

                    _orderStore.PlaceOrder(order);

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

        public IEnumerable<OrderDto> GetUserOrders(int userId)
        {
            var userOrders = _orderStore.GetUserOrders(userId);

            var orderDtos = userOrders.Select(x => new OrderDto()
            {
                UserId = x.UserId,
                BookId = x.BookId,
                Date = x.OrderDate
            }).ToList();

            var itemsFromOrders = _itemStore.GetItemsByIds(orderDtos.Select(x => x.BookId));

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
