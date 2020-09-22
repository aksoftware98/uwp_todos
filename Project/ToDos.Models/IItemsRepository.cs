using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDos.Models
{
    public interface IItemsRepository
    {
        Task<ToDoItem> GetByIdAsync(string id);

        Task<ToDoItem> InsertItemAsync(ToDoItem item);

        Task CommitChangesAsync();

        Task<ToDoItem> DeleteItemAsync(string id);

        Task<IEnumerable<ToDoItem>> GetDayItemsAsync(DateTime dayDate, string userId);

    }
}
