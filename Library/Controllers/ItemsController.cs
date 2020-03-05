using System.Collections.Generic;
using Library.Services.Dtos;
using Library.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemsController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("getItems")]
        public IEnumerable<ItemDto> GetItems()
        {
            return _itemService.GetItems();
        }
    }
}