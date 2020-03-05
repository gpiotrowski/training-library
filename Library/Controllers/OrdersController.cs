using System;
using System.Collections.Generic;
using System.Linq;
using Library.Data;
using Library.Dtos;
using Library.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public readonly UserStore userStore;
        private readonly OrderStore orderStore;
        private readonly ItemStore itemStore;

        public OrdersController(ItemStore itemStore, UserStore userStore, OrderStore orderStore)
        {
            this.userStore = userStore;
            this.orderStore = orderStore;
            this.itemStore = itemStore;
        }

        [HttpPost("placeOrder")]
        public IActionResult PlaceOrder(NewOrderDto orderDto)
        {
            var requestedBook = itemStore.GetItemById(orderDto.BookId);
            if (requestedBook?.AvailableQuantity > 0)
            {
                var user = userStore.GetUserById(orderDto.UserId);
                var inProgressOrders = orderStore.GetUserOrderInProgressQty(orderDto.UserId);

                if (user.BookLimit >= inProgressOrders)
                {
                    requestedBook.AvailableQuantity--;

                    userStore.SaveUser(user);

                    var order = new Order()
                    {
                        BookId = orderDto.BookId,
                        IsReturned = false,
                        OrderDate = DateTime.UtcNow,
                        ReturnDate = null,
                        UserId = orderDto.UserId
                    };

                    orderStore.PlaceOrder(order);

                    return Ok();
                }
                else
                {
                    return BadRequest("Book limit exceeded");
                }
            }
            
            return BadRequest("Requested book is not available");
        }

        [HttpGet("checkMyOrders")]
        public IEnumerable<OrderDto> CheckMyOrders(int userId)
        {
            var userOrders = orderStore.GetUserOrders(userId);

            var orderDtos = userOrders.Select(x => new OrderDto()
            {
                UserId = x.UserId,
                BookId = x.BookId,
                Date = x.OrderDate
            }).ToList();

            var itemsFromOrders = itemStore.GetItemsByIds(orderDtos.Select(x => x.BookId));

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