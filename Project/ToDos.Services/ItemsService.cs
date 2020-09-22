using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDos.Models;
using ToDos.Models.Dtos;
using ToDos.Models.Response;

namespace ToDos.Services
{

    public class ItemsService : BaseService, IItemsService
    {

        private readonly IItemsRepository _itemsRepo;

        public ItemsService(IItemsRepository itemsRepo)
        {
            _itemsRepo = itemsRepo;
        }

        public async Task<IOperationResponse<ToDoItemDetail>> CancelItemAsync(string id, string userId)
        {
            var item = await _itemsRepo.GetByIdAsync(id);
            if (item == null)
                return NotFound<ToDoItemDetail>("Item not found");

            item.IsCanceled = true;
            item.CancelationDate = DateTime.UtcNow;
            item.ModificationDate = DateTime.UtcNow;

            await _itemsRepo.CommitChangesAsync(); 

            return Success("Item canceled successfully!", item.ToItemDetail());
        }

        public async Task<IOperationResponse<ToDoItemDetail>> CreateItemAsync(ToDoItemDetail item, string userId)
        {
            var todo = new ToDoItem
            {
                Id = Guid.NewGuid().ToString(),
                Category = item.Category,
                Description = item.Descrption,
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
                Points = item.Points,
                Quality = item.Quality,
                UserId = userId,
                TaskDate = item.TaskDate.Value.ToUniversalTime(),
            };

            await _itemsRepo.InsertItemAsync(todo);
            await _itemsRepo.CommitChangesAsync();

            item.Id = todo.Id;
            item.CreationDate = todo.CreationDate;
            return Success("Item added successfully", item); 
        }

        public async Task<IOperationResponse<ToDoItemDetail>> DeleteItemAsync(string id, string userId)
        {
            var item = await _itemsRepo.GetByIdAsync(id);

            if (item == null)
                NotFound<ToDoItemDetail>("Item you are trying to delete is not existing");

            item.IsDeleted = true;
            item.DeletionDate = DateTime.UtcNow;

            // Save 
            await _itemsRepo.CommitChangesAsync();
            return Success("Item has been deleted successfully!", new ToDoItemDetail
            {
                
            });
        }

        public async Task<IEnumerable<ToDoItemDetail>> GetDayItemsAsync(DateTime day, string userId)
        {
            var dailyItems = await _itemsRepo.GetDayItemsAsync(day, userId);

            return dailyItems.Select(i => i.ToItemDetail());
        }

        public async Task<IOperationResponse<ToDoItemDetail>> MarkItemAsDoneAsync(string id, string userId)
        {
            var item = await _itemsRepo.GetByIdAsync(id);
            if (item == null)
                return NotFound<ToDoItemDetail>("Item not found");

            item.IsDone = true;
            item.AchievingDate = DateTime.UtcNow;

            await _itemsRepo.CommitChangesAsync();

            return Success("Item marked as done successfully!", item.ToItemDetail()); 
        }

        public async Task<IOperationResponse<ToDoItemDetail>> MarkItemAsUndoneAsync(string id, string userId)
        {
            var item = await _itemsRepo.GetByIdAsync(id);
            if (item == null)
                return NotFound<ToDoItemDetail>("Item not found");

            item.IsDone = false;
            item.AchievingDate = null;
            item.ModificationDate = DateTime.UtcNow; 

            await _itemsRepo.CommitChangesAsync();

            return Success("Item marked as done successfully!", item.ToItemDetail());
        }

        public async Task<IOperationResponse<ToDoItemDetail>> UpdateItemAsync(ToDoItemDetail model, string userId)
        {
            var item = await _itemsRepo.GetByIdAsync(model.Id);

            if (item == null)
                return NotFound<ToDoItemDetail>("Item not found");

            item.Description = model.Descrption;
            item.Category = model.Category;
            item.ModificationDate = DateTime.UtcNow;
            item.Points = model.Points;
            item.Quality = model.Quality;

            await _itemsRepo.CommitChangesAsync();

            return Success("Item updated successfully!", model); 
        }
    }


}
