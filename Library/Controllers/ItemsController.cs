using System.Collections.Generic;
using System.Linq;
using Library.Dtos;
using Library.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemStore store;

        public ItemsController(ItemStore store)
        {
            this.store = store;
        }

        [HttpGet("getItems")]
        public IEnumerable<ItemDto> GetItems()
        {
            var allItems = store.GetItems();

            return allItems.Select(x => new ItemDto()
            {
                Id = x.Id,
                Name =  x.Name,
                Author = x.Author,
                Price = x.Price,
                AvailableQuantity = x.AvailableQuantity
            });
        }
    }
}