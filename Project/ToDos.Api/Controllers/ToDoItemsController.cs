using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDos.Models;
using ToDos.Models.Dtos;
using ToDos.Services;

namespace ToDos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : BaseController
    {

        private readonly IItemsService _itemsService; 

        public ToDoItemsController(IItemsService itemsSerivce)
        {
            _itemsService = itemsSerivce;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string day)
        {
            var result = await _itemsService.GetDayItemsAsync(DateTime.ParseExact(day, "MMddyyyy", null), UserId);
            return Ok(result); 
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoItemDetail item)
        {
            var result = await _itemsService.CreateItemAsync(item, UserId); 

            return Ok(result.Content);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ToDoItemDetail item)
        {
            var result = await _itemsService.UpdateItemAsync(item, UserId);

            return Ok(result.Content); 
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _itemsService.DeleteItemAsync(id, UserId);
            return Ok(result.Message); 
        }

        [HttpPost("Check/{id}")]
        public async Task<IActionResult> CheckItem(string id)
        {
            var result = await _itemsService.MarkItemAsDoneAsync(id, UserId);
            return Ok(result.Content); 
        }

        [HttpPost("Uncheck/{id}")]
        public async Task<IActionResult> UncheckItem(string id)
        {
            var result = await _itemsService.MarkItemAsUndoneAsync(id, UserId);
            return Ok(result.Content); 
        }


    }

    public class BaseController : ControllerBase
    {
        protected string UserId => User.Identity.IsAuthenticated ? User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value : "Test user";
    }
}
