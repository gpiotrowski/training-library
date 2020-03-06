using System;
using System.Collections.Generic;
using Library.Leases.Application;
using Library.Leases.Application.Dtos;
using Library.Leases.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly GetReaderLeasesService _getReaderLeasesService;
        private readonly LeaseBookService _leaseBookService;

        public OrdersController(GetReaderLeasesService getReaderLeasesService, LeaseBookService leaseBookService)
        {
            _getReaderLeasesService = getReaderLeasesService;
            _leaseBookService = leaseBookService;
        }

        [HttpPost("placeOrder")]
        public IActionResult PlaceOrder(NewLeaseDto leaseDto)
        {
            var operationStatus = _leaseBookService.LeaseBook(leaseDto);

            if (!operationStatus.Success)
            {
                return BadRequest(operationStatus.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet("checkMyOrders")]
        public IEnumerable<LeaseDto> CheckMyOrders(Guid readerId)
        {
            return _getReaderLeasesService.GetReaderLeases(readerId);
        }
    }
}