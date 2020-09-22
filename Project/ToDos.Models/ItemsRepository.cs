using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDos.Models
{
    public class ItemsRepository : IItemsRepository
    {

        private readonly ApplicationDbContext _db;
        public ItemsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ToDoItem> GetByIdAsync(string id)
        {
            return await _db.ToDoItems.FindAsync(id);
        }

        public async Task<ToDoItem> DeleteItemAsync(string id)
        {
            var result = await _db.ToDoItems.FindAsync(id); 
            if(result != null)
            {
                result.IsDeleted = true;
            }
            
            return result; 
        }

        public async Task<IEnumerable<ToDoItem>> GetDayItemsAsync(DateTime dayDate, string userId)
        {
            var minDate = dayDate;
            var maxDate = dayDate.AddHours(24); 
            var results = _db.ToDoItems.Where(i => i.UserId == userId && (i.TaskDate >= minDate || i.TaskDate <= maxDate));

            return await results.ToListAsync();
        }

        public async Task<ToDoItem> InsertItemAsync(ToDoItem item)
        {
            await _db.ToDoItems.AddAsync(item);
            
            return item; 
        }

        public async Task CommitChangesAsync()
        {
            await _db.SaveChangesAsync(); 
        }

    }
}
