using System;
using System.Collections.Generic;
using Library.Leases.Domain.Dtos;
using Library.Leases.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly LeaseService _leaseService;

        public OrdersController(LeaseService leaseService)
        {
            _leaseService = leaseService;
        }

        [HttpPost("placeOrder")]
        public IActionResult PlaceOrder(NewLeaseDto leaseDto)
        {
            var operationStatus = _leaseService.LeaseBook(leaseDto);

            if (!operationStatus.Success)
            {
                return BadRequest(operationStatus.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet("checkMyOrders")]
        public IEnumerable<LeaseDto> CheckMyOrders(Guid readerId)
        {
            return _leaseService.GetReaderOrders(readerId);
        }
    }
}