using System;
using System.Collections.Generic;
using System.Linq;
using Library.Services.Dtos;
using Library.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("placeOrder")]
        public IActionResult PlaceOrder(NewOrderDto orderDto)
        {
            var operationStatus = _orderService.PlaceOrder(orderDto);

            if (!operationStatus.Success)
            {
                return BadRequest(operationStatus.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet("checkMyOrders")]
        public IEnumerable<OrderDto> CheckMyOrders(int userId)
        {
            return _orderService.GetUserOrders(userId);
        }
    }
}