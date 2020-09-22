using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models.Dtos;
using ToDos.Models.Response;

namespace ToDos.Services
{
    public interface IItemsService
    {

        Task<IOperationResponse<ToDoItemDetail>> CreateItemAsync(ToDoItemDetail item, string userId);

        Task<IOperationResponse<ToDoItemDetail>> UpdateItemAsync(ToDoItemDetail item, string userId);

        Task<IOperationResponse<ToDoItemDetail>> DeleteItemAsync(string id, string userId);

        Task<IEnumerable<ToDoItemDetail>> GetDayItemsAsync(DateTime day, string userId);

        Task<IOperationResponse<ToDoItemDetail>> MarkItemAsDoneAsync(string id, string userId);

        Task<IOperationResponse<ToDoItemDetail>> MarkItemAsUndoneAsync(string id, string userId);

        Task<IOperationResponse<ToDoItemDetail>> CancelItemAsync(string id, string userId); 
        
    }
}
