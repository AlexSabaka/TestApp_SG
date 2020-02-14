using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppApi.Data.Entities;
using AppApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IEnumerable<ProductItem> GetAll()
        {
            return _itemService.GetAll();
        }

        [HttpGet("{id}")]
        public ProductItem Get(int id)
        {
            return _itemService.Get(id);
        }
    }
}
