using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDos.Models;

namespace ToDos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItems : ControllerBase
    {

        private readonly IItemsRepository _itemsRepo;

        public ToDoItems(IItemsRepository itemsRepo)
        {
            _itemsRepo = itemsRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoItem item)
        {
            await _itemsRepo.InsertItemAsync(item);

            await _itemsRepo.CommitChangesAsync();
           
            return Ok(item);
        }

    }
}
